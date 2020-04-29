select s.title, replace(sc.content_html, X '0A', '^N')
  from subitem s
  join subitem_content sc
	on sc.subitem_id = s.id
 order by s.position;