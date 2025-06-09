Imports System.Net.Http ' Para HttpClient
Imports Newtonsoft.Json ' Para JsonConvert
Imports System.Threading.Tasks ' Para Async/Await

Public Class Form1
    Private TipoSelecionado As Pacote
    Private Enum TipoPacote
        GitIgnore
        LinhaComando
    End Enum

    Private Structure Pacote
        ' Certifique-se de que os nomes das propriedades correspondem aos nomes no JSON (case-sensitive)
        Public Property nomePacote As String
        Public Property tipo As TipoPacote
        Public Property UrlGitIgnore As String
        Public Property LinhaComando As String
    End Structure

    Private Opcoes As List(Of Pacote)

    ' URL fixa do JSON
    Private Const JSON_URL As String = "https://example.com/seu_pacotes.json" ' <--- ALtere esta URL para a sua URL real do JSON!

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Inicialize a lista Opcoes
        Opcoes = New List(Of Pacote)()

        Try
            Using client As New HttpClient()
                Console.WriteLine($"Baixando opções de: {JSON_URL}")
                Dim jsonString As String = Await client.GetStringAsync(JSON_URL)
                Console.WriteLine("Opções baixadas com sucesso.")

                ' Desserializa o JSON para uma lista de Pacote
                Dim pacotesDoJson As List(Of Pacote) = JsonConvert.DeserializeObject(Of List(Of Pacote))(jsonString)

                If pacotesDoJson IsNot Nothing AndAlso pacotesDoJson.Any() Then
                    Opcoes.AddRange(pacotesDoJson) ' Adiciona todos os pacotes do JSON à sua lista Opcoes
                    Console.WriteLine($"Total de {Opcoes.Count} pacotes carregados do JSON.")
                Else
                    Console.WriteLine("Nenhum pacote encontrado no JSON ou JSON vazio.")
                End If
            End Using

        Catch ex As HttpRequestException
            MessageBox.Show($"Erro de rede ao baixar o JSON: {ex.Message}", "Erro de Carregamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Erro de rede ao baixar o JSON: {ex.Message}")
        Catch ex As JsonException
            MessageBox.Show($"Erro ao processar o JSON: {ex.Message}", "Erro de Carregamento", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Erro ao processar o JSON: {ex.Message}")
        Catch ex As Exception
            MessageBox.Show($"Ocorreu um erro inesperado: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Console.WriteLine($"Ocorreu um erro inesperado: {ex.Message}")
        End Try

        ' Exemplo de como você pode usar as opções carregadas (apenas para teste)
        If Opcoes.Any() Then
            Console.WriteLine("Primeira opção carregada:")
            Console.WriteLine($"- Nome: {Opcoes(0).nomePacote}")
            Console.WriteLine($"- Tipo: {Opcoes(0).tipo}")
        End If
    End Sub
    'KryptonListBox1.Items.Clear()
    'For Each cadaPacote As Pacote In Opcoes
    '    KryptonListBox1.Items.Add(cadaPacote.nomePacote)
    'Next

    Private Sub KryptonButton1_Click(sender As Object, e As EventArgs) Handles KryptonButton1.Click
        For Each cadaPacote As Pacote In Opcoes
            If cadaPacote.nomePacote.Equals(KryptonListBox1.SelectedItem.ToString) Then
                TipoSelecionado = cadaPacote
            End If
        Next

        TabControl1.SelectedTab = TabPage2
    End Sub

    Private Sub KryptonButton2_Click(sender As Object, e As EventArgs) Handles KryptonButton2.Click
        Dim openFileDialog As New OpenFileDialog()

        ' Configura o título da caixa de diálogo
        openFileDialog.Title = "Selecione um Arquivo"
        If Not TipoSelecionado.Equals(String.Empty) Then
            openFileDialog.Filter = TipoSelecionado.filtroArquivo

            openFileDialog.FilterIndex = 1

        End If

        ' Exibe a caixa de diálogo
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            ' Se o usuário clicou em OK, o arquivo selecionado estará em openFileDialog.FileName
            Dim filePath As String = openFileDialog.FileName
            KryptonLabel2.Text = $"Você selecionou o arquivo: {filePath}"
        End If
    End Sub

    Private Async Sub KryptonButton3_Click(sender As Object, e As EventArgs) Handles KryptonButton3.Click
        If IO.File.Exists(KryptonLabel2.Text) Then
            TabControl1.SelectedTab = TabPage3
            Dim originalZipPath As String = KryptonLabel2.Text ' Substitua pelo caminho real do seu ZIP

            Dim processor As New ZipProcessor()
            Dim cleanedZipPath As String = Await processor.ProcessAndCleanZip(originalZipPath, TipoSelecionado.UrlGitIgnore)

            If Not String.IsNullOrEmpty(cleanedZipPath) Then
                MessageBox.Show($"O arquivo ZIP limpo foi criado em: {cleanedZipPath}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Falha ao processar e limpar o arquivo ZIP. Verifique a saída do console para detalhes.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub
End Class
