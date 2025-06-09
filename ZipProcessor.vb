Imports System.IO
Imports System.IO.Compression
Imports System.Net.Http ' Necessário para HttpClient
Imports System.Threading.Tasks ' Necessário para Task e async/await

Public Class ZipProcessor

    ''' <summary>
    ''' Descompacta um arquivo ZIP, limpa o conteúdo com base em um .gitignore de uma URL e compacta novamente.
    ''' </summary>
    ''' <param name="zipFilePath">O caminho completo para o arquivo ZIP de origem.</param>
    ''' <param name="gitIgnoreUrl">A URL do arquivo .gitignore a ser usado para limpeza.</param>
    ''' <returns>O caminho completo para o novo arquivo ZIP limpo, ou Nothing em caso de erro.</returns>
    Public Async Function ProcessAndCleanZip(ByVal zipFilePath As String, ByVal gitIgnoreUrl As String) As Task(Of String) ' Torna a função assíncrona
        If Not File.Exists(zipFilePath) Then
            Console.WriteLine($"Erro: O arquivo ZIP '{zipFilePath}' não foi encontrado.")
            Return Nothing
        End If

        Dim tempExtractionPath As String = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())
        Dim tempCleanGitIgnorePath As String = Path.Combine(tempExtractionPath, ".gitignore") ' Caminho temporário para o .gitignore baixado
        Dim tempCleanZipPath As String = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(zipFilePath) & "_cleaned.zip")

        Try
            ' 1. Descompactar o ZIP
            Console.WriteLine($"Descompactando '{zipFilePath}' para '{tempExtractionPath}'...")
            ZipFile.ExtractToDirectory(zipFilePath, tempExtractionPath)
            Console.WriteLine("Descompactação concluída.")

            ' 2. Baixar o .gitignore da URL
            Console.WriteLine($"Baixando .gitignore de: '{gitIgnoreUrl}'...")
            Using client As New HttpClient()
                Dim gitIgnoreContent As String = Await client.GetStringAsync(gitIgnoreUrl)
                File.WriteAllText(tempCleanGitIgnorePath, gitIgnoreContent)
                Console.WriteLine($"Conteúdo do .gitignore salvo em: '{tempCleanGitIgnorePath}'")
            End Using

            ' 3. Executar limpeza com base no .gitignore baixado
            If File.Exists(tempCleanGitIgnorePath) Then
                Console.WriteLine("Iniciando limpeza baseada no .gitignore...")
                ApplyGitIgnoreRules(tempExtractionPath, tempCleanGitIgnorePath)
                Console.WriteLine("Limpeza baseada no .gitignore concluída.")
            Else
                Console.WriteLine("Erro: Arquivo .gitignore temporário não encontrado após download.")
            End If

            ' 4. Compactar o diretório limpo novamente
            Console.WriteLine($"Compactando o diretório limpo para '{tempCleanZipPath}'...")
            ZipFile.CreateFromDirectory(tempExtractionPath, tempCleanZipPath)
            Console.WriteLine("Re-compactação concluída.")

            Return tempCleanZipPath

        Catch ex As HttpRequestException
            Console.WriteLine($"Erro ao baixar o .gitignore da URL '{gitIgnoreUrl}': {ex.Message}")
            Return Nothing
        Catch ex As Exception
            Console.WriteLine($"Ocorreu um erro durante o processamento do ZIP: {ex.Message}")
            Return Nothing
        Finally
            ' Limpar diretórios e arquivos temporários
            If Directory.Exists(tempExtractionPath) Then
                Try
                    Directory.Delete(tempExtractionPath, True) ' O True significa exclusão recursiva
                    Console.WriteLine($"Diretório temporário '{tempExtractionPath}' removido.")
                Catch ex As Exception
                    Console.WriteLine($"Erro ao remover diretório temporário '{tempExtractionPath}': {ex.Message}")
                End Try
            End If
            ' Não exclua tempCleanZipPath aqui se a operação foi bem-sucedida, pois é o resultado
        End Try
    End Function

    ''' <summary>
    ''' Aplica as regras simplificadas do .gitignore para remover arquivos e pastas.
    ''' </summary>
    ''' <param name="directoryToClean">O diretório a ser limpo.</param>
    ''' <param name="gitIgnorePath">O caminho para o arquivo .gitignore.</param>
    Private Sub ApplyGitIgnoreRules(ByVal directoryToClean As String, ByVal gitIgnorePath As String)
        Dim ignorePatterns As New List(Of String)()

        ' Ler as regras do .gitignore
        For Each line As String In File.ReadLines(gitIgnorePath)
            Dim trimmedLine As String = line.Trim()
            If Not String.IsNullOrEmpty(trimmedLine) AndAlso Not trimmedLine.StartsWith("#") Then
                ' Remover barras no início e no fim para simplificar o matching
                If trimmedLine.StartsWith("/") Then trimmedLine = trimmedLine.Substring(1)
                If trimmedLine.EndsWith("/") Then trimmedLine = trimmedLine.Substring(0, trimmedLine.Length - 1)
                ignorePatterns.Add(trimmedLine.Replace("/", Path.DirectorySeparatorChar.ToString())) ' Normalizar para separador de diretório do SO
            End If
        Next

        If ignorePatterns.Count = 0 Then
            Console.WriteLine("Nenhuma regra de exclusão válida encontrada no .gitignore.")
            Return
        End If

        ' Listar todos os arquivos e diretórios recursivamente
        Dim allFiles As String() = Directory.GetFiles(directoryToClean, "*", SearchOption.AllDirectories)
        Dim allDirectories As String() = Directory.GetDirectories(directoryToClean, "*", SearchOption.AllDirectories)

        ' Processar as regras de exclusão
        For Each pattern As String In ignorePatterns
            Console.WriteLine($"Aplicando regra de exclusão: '{pattern}'")

            ' Lidar com arquivos
            For Each filePath As String In allFiles
                Dim relativePath As String = GetRelativePath(filePath, directoryToClean)
                ' Usar o Path.GetFileName para corresponder a nomes de arquivo sem caminho
                If relativePath.Equals(pattern, StringComparison.OrdinalIgnoreCase) OrElse
                   Path.GetFileName(relativePath).Equals(pattern, StringComparison.OrdinalIgnoreCase) OrElse
                   (pattern.Contains("*") AndAlso IsMatchWithWildcard(relativePath, pattern)) Then ' Usar função de correspondência de curinga

                    Try
                        File.Delete(filePath)
                        Console.WriteLine($"Excluído arquivo: {relativePath}")
                    Catch ex As Exception
                        Console.WriteLine($"Não foi possível excluir o arquivo '{relativePath}': {ex.Message}")
                    End Try
                End If
            Next

            ' Lidar com diretórios
            For Each dirPath As String In allDirectories
                Dim relativePath As String = GetRelativePath(dirPath, directoryToClean)
                ' Verificar se o diretório ou um de seus pais é uma correspondência
                If relativePath.Equals(pattern, StringComparison.OrdinalIgnoreCase) OrElse
                   (pattern.EndsWith("/") AndAlso relativePath.StartsWith(pattern.Substring(0, pattern.Length - 1), StringComparison.OrdinalIgnoreCase)) OrElse
                   (pattern.Contains("*") AndAlso IsMatchWithWildcard(relativePath, pattern)) Then ' Usar função de correspondência de curinga

                    Try
                        ' Apenas excluir se o diretório ainda existir (pode ter sido excluído por uma regra anterior)
                        If Directory.Exists(dirPath) Then
                            Directory.Delete(dirPath, True) ' Exclusão recursiva
                            Console.WriteLine($"Excluído diretório e conteúdo: {relativePath}")
                        End If
                    Catch ex As Exception
                        Console.WriteLine($"Não foi possível excluir o diretório '{relativePath}': {ex.Message}")
                    End Try
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' Obtém o caminho relativo de um arquivo/diretório em relação a um diretório base.
    ''' </summary>
    Private Function GetRelativePath(ByVal fullPath As String, ByVal basePath As String) As String
        ' Garante que o basePath termine com um separador de diretório para Path.GetRelativePath
        If Not basePath.EndsWith(Path.DirectorySeparatorChar.ToString()) Then
            basePath += Path.DirectorySeparatorChar
        End If
        ' Usa Uri para garantir compatibilidade com diferentes sistemas operacionais e casos complexos
        Dim baseUri As New Uri(basePath)
        Dim fullUri As New Uri(fullPath)
        Return Uri.UnescapeDataString(baseUri.MakeRelativeUri(fullUri).ToString()).Replace("/", Path.DirectorySeparatorChar.ToString())
    End Function

    ''' <summary>
    ''' Função auxiliar para corresponder strings com curingas '*' (simplificada).
    ''' </summary>
    ''' <remarks>
    ''' Esta é uma implementação muito básica e não cobre todas as nuances de Regex ou padrões Gitignore.
    ''' </remarks>
    Private Function IsMatchWithWildcard(ByVal input As String, ByVal pattern As String) As Boolean
        Dim regexPattern As String = System.Text.RegularExpressions.Regex.Escape(pattern).Replace("\*", ".*").Replace("\?", ".")
        Return System.Text.RegularExpressions.Regex.IsMatch(input, "^" & regexPattern & "$", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
    End Function

End Class