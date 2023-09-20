USE library;

-- Показать идентификаторы всех «самых читающих читателей»,
-- взявших в библиотеке больше всего книг.

DECLARE @LAB1_MAX_COUNT AS INT;
SELECT TOP(1) @LAB1_MAX_COUNT = [sb_count]
	FROM (
		SELECT [sb_subscriber], COUNT(*) AS [sb_count]
			FROM [subscriptions]
			GROUP BY [sb_subscriber]
	) AS TEMP
	ORDER BY [sb_count] DESC;
SELECT [sb_subscriber]
	FROM (
		SELECT [sb_subscriber], COUNT(*) AS [sb_count] 
			FROM [subscriptions]
			GROUP BY [sb_subscriber]
		) AS TEMP 
	WHERE [sb_count] = @LAB1_MAX_COUNT;

-- Показать идентификатор «читателя-рекордсмена», 
-- взявшего в библиотеке больше книг, чем любой другой читатель.

SELECT TOP(1) [sb_subscriber]
	FROM (
		SELECT [sb_subscriber], COUNT(*) AS [sb_count]
			FROM [subscriptions]
			GROUP BY [sb_subscriber]
	) AS TEMP
	ORDER BY [sb_count] DESC;

-- Показать, сколько в среднем экземпляров книг есть в библиотеке.

SELECT AVG([b_quantity]) AS [b_average_count] FROM [books];

-- Показать в днях, сколько в среднем времени читатели уже зарегистрированы в 
-- библиотеке (временем регистрации считать диапазон от первой даты получения читателем книги до текущей даты).

SELECT AVG([sb_days]) as [sb_average_period] FROM (
	SELECT DATEDIFF(day, MIN(sb_start), GETDATE()) AS [sb_days]
		FROM [subscriptions] 
		GROUP BY [sb_subscriber]
	) AS TEMP;

-- Показать, сколько книг было возвращено и не возвращено в библиотеку 
-- (СУБД должна оперировать исходными значениями поля sb_is_active (т.е. «Y» и «N»), 
-- а после подсчёта значения «Y» и «N» должны быть преобразованы в «Returned» и «Not returned»).

DECLARE @LAB1_Y AS INT;
DECLARE @LAB1_N AS INT;
SELECT @LAB1_Y = COUNT(*) 
	FROM [subscriptions]
	WHERE [sb_is_active] = 'Y';
SELECT @LAB1_N = COUNT(*) 
	FROM [subscriptions]
	WHERE [sb_is_active] = 'N';
SELECT @LAB1_Y AS [Returned],
       @LAB1_N AS [Not returned];