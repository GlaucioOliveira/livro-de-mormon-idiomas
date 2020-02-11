# Livro de Mórmon - Estudo de Idiomas

A idéia do projeto surgiu de um plano de estudo que tenho com minha esposa para estudo do Livro de Mórmon em Inglês e Espanhol.

Ao invés de ficar alternando entre idiomas no aplicativo *Gospel Library*, resolvi consolidar todo o conteúdo.

No repository tem uma solution do Visual Studio (.NET Core) ( [/Extrator](/extrator) ) que manipula alguns dados extraídos do banco de dados (SQLite) que é usado pelo aplicativo Gospel Library, para gerar paginas HTML agrupando os versículos por idiomas (Português, Inglês e Espanhol).

A query utilizada para manipular gerar os arquivos *dump* com os capítulos e versículo foi a:

```sql
select s.title, replace(sc.content_html, X '0A', '^N')
  from subitem s
  join subitem_content sc
	on sc.subitem_id = s.id
 order by s.position;
```

Para gerar o arquivo .mobi (para ler no Kindle), basta após executar o *extrator*, importar no Calibre o arquivo `index.html` e depois converter o livro para o formato .mobi ou .epub.
