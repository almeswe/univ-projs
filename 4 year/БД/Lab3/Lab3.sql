--ѕоказать список книг, у которых более одного автора.
WITH [temp] AS (
	SELECT [books].[b_id], COUNT(*) as [b_count] FROM [books]
		JOIN [m2m_books_authors] ON 
			[books].[b_id] = [m2m_books_authors].[b_id]
	GROUP BY [books].[b_id]
)
SELECT [books].[b_name], [b_count] from [temp]
	JOIN [books] ON [books].[b_id] = [temp].[b_id]
	WHERE [b_count] > 1;


--ѕоказать читаемость жанров, т.е. все жанры и то количество раз, которое книги этих жанров были вз€ты читател€ми.
WITH [temp] AS (
	SELECT [g_name], [sb_book] FROM [subscriptions]
		JOIN [m2m_books_genres] ON [sb_book] = [b_id]
		JOIN [genres] ON [genres].[g_id] = [m2m_books_genres].[g_id]
)
SELECT [g_name], COUNT(sb_book) AS [g_score] FROM [temp] 
	GROUP BY [g_name]
	ORDER BY [g_score] DESC;

--ѕоказать всех читателей, не вернувших книги, и количество невозвращЄнных книг по каждому такому читателю.
WITH [temp] AS (
	SELECT [sb_subscriber], [sb_book] FROM [subscriptions]
		WHERE [sb_is_active] = 'Y'
)
SELECT [subscribers].[s_id], [s_name], [s_count] FROM (
	SELECT [sb_subscriber] AS [s_id], COUNT([sb_book]) AS [s_count] FROM [temp]
		GROUP BY [sb_subscriber]
) temp2
JOIN [subscribers] ON [subscribers].[s_id] = [temp2].[s_id]
ORDER BY [s_count] DESC;

--ѕоказать последнюю книгу, которую каждый из читателей вз€л в библиотеке.
WITH [temp] AS (
	SELECT [sb_subscriber], MAX(DATEDIFF(DAY, [sb_start], GETDATE())) AS [sb_days] FROM [subscriptions]
		GROUP BY [sb_subscriber]
)
SELECT [s_id], [s_name], [b_name] FROM (
	SELECT [temp].[sb_subscriber], [sb_book], [sb_days] FROM [temp]	
		JOIN [subscriptions] ON (
			([temp].[sb_subscriber] = [subscriptions].[sb_subscriber]) AND
			([temp].[sb_days] = DATEDIFF(DAY, [sb_start], GETDATE()))
		)
) AS temp2
JOIN [books] ON [books].[b_id] = [temp2].sb_book
JOIN [subscribers] ON [subscribers].[s_id] = [temp2].sb_subscriber;

--ѕоказать читател€, последним вз€вшего в библиотеке книгу
WITH [temp] AS (
	SELECT TOP (1) [sb_subscriber] FROM [subscriptions]
		WHERE [sb_is_active] = 'Y'
		ORDER BY DATEDIFF(DAY, [sb_finish], GETDATE())
)
SELECT [s_name] FROM [temp]
	JOIN [subscribers] ON [s_id] = [sb_subscriber];