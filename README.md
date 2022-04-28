<h1>Sistema de Gerenciamento de Rotas</h1>
  
  <p>O principal objetivo do sistema e conseguir cadastrar um time e consumir um excel, para criar um arquivo (.doc) que contenha informacoes dos servicos propostos no dia!</p>


<h2>Tecnologia utilizada no projeto</h2>
<li>.Net 5.0
<h3>Banco de dados</h3>
<li>MongoDB
<li>SQL Server


<h2>Bibliotecas que foram utilizadas</h2>

<li>EPPlus 5.8.9
<li>Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 5.0.13
<li>Microsoft.AspNetCore.Identity.EntityFrameworkCore 5.0.16
<li>Microsoft.AspNetCore.Identity.UI 5.0.16
<li>Microsoft.AspNetCore.Mvc 2.2.0
<li>Microsoft.EntityFrameworkCore.SqlServer 5.0.16
<li>Microsoft.EntityFrameworkCore.Tools 5.0.16
<li>Microsoft.VisualStudio.Web.CodeGeneration.Design 5.0.2
<li>MongoDB.Bson 2.15.0
<li>MongoDB.Driver 2.15.0
<li>Newtonsoft.Json 13.0.1
<li>Swashbucle.AspNetCore 5.6.3
  
 
<h2>
  Como utilizar:
</h2>
  
<li>
  Tenha um servidor rodando MongoDB na porta padrão para utilização.
<li>
  Selecione  o projeto "MVC" e rode o comando <b>update-database</b> no console de gerenciamento de pacotes para criar o banco de dados SQL, que será utilizado para realizar autenticação.
<li>
  Para criar um usuário que é capaz de criar outros usuários (administrador), utilize o email "admin@hotmail.com" (só pode haver um administrador na plataforma).
<li>
  Apos efetuar o login e entrar na plataforma. Para que o sistema funcione corretamente cadastre na ordem Pessoas depois Cidades e Times, pois para um time existir ele necessita de pessoas e uma cidade.

  
<h2>
  Feito:
</h2>


<li>
  API de Pessoas.
<li>
  API de Times.
<li>
  API de Cidades.
<li>
  MVC de Pessoa (Listagem, Criação, Edição, Exclusão).
<li>
  MVC de Time (Listagem, Criação, Edição, Exclusão).
<li>
  MVC de Cidades (Listagem, Criação, Edição, Exclusão).
<li>
  MVC de Documento (Upload).
<li>
  Leitura do Arquivo Excel (.xlsx).
<li>
  Selecionar os dados da planilha que serão exibidos.
<li>
  Selecionar o tipo de serviço disponível.
<li>
  Gerar documento Word (.docx) com os times e suas rotas.
<li>
  MVC login.

