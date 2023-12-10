-- Создать хранимую процедуру, которая:
-- * добавляет каждой книге два случайных жанра;
-- * отменяет совершённые действия, если в процессе работы хотя бы одна операция вставки завершилась ошибкой в силу дублирования значения первичного ключа таблицы «m2m_books_genres» (т.е. у такой книги уже был такой жанр).

DROP PROCEDURE ADD_GENRES;

CREATE PROCEDURE ADD_GENRES AS
BEGIN
	DECLARE @BOOK_ID INT;
	DECLARE @GENRE_ID1 INT;
	DECLARE @GENRE_ID2 INT;
	BEGIN TRY
		BEGIN TRANSACTION;
			DECLARE BOOK_CURSOR CURSOR FOR SELECT b_id FROM books;
			OPEN BOOK_CURSOR;
			FETCH NEXT FROM BOOK_CURSOR INTO @BOOK_ID;
			WHILE @@FETCH_STATUS = 0 BEGIN
				SELECT TOP 1 @GENRE_ID1 = g_id FROM genres ORDER BY NEWID();
				SELECT TOP 1 @GENRE_ID2 = g_id FROM genres ORDER BY NEWID();
				INSERT INTO m2m_books_genres (b_id, g_id) VALUES (@BOOK_ID, @GENRE_ID1);
				INSERT INTO m2m_books_genres (b_id, g_id) VALUES (@BOOK_ID, @GENRE_ID2);
				FETCH NEXT FROM BOOK_CURSOT INTO @BOOK_ID;
			END;
			CLOSE BOOK_CURSOR;
			DEALLOCATE BOOK_CURSOR;
			PRINT 'COMMIT';
		COMMIT;
	END TRY
	BEGIN CATCH
		PRINT 'ROLLBACK';
		ROLLBACK;
	END CATCH;
END;

EXEC ADD_GENRES;

-- Создать хранимую процедуру, которая:
-- увеличивает значение поля «b_quantity» для всех книг в два раза;
-- отменяет совершённое действие, если по итогу выполнения операции среднее количество экземпляров книг превысит значение 50

DROP PROCEDURE QUANTITY_INCREMENTOR;

CREATE PROCEDURE QUANTITY_INCREMENTOR AS
BEGIN
	DECLARE @BOOK_ID INT;
	DECLARE @AVERAGE INT;
	BEGIN TRANSACTION 
		UPDATE books SET b_quantity = b_quantity * 2;
		SELECT @AVERAGE = AVG(b_quantity) FROM books GROUP BY b_id;
		IF @AVERAGE >= 50 BEGIN
			PRINT 'ROLLBACK';
			ROLLBACK TRANSACTION;
			RETURN;
		END;
		PRINT 'COMMIT';
	COMMIT TRANSACTION;
END;

EXEC QUANTITY_INCREMENTOR;

-- Написать запросы, которые, будучи выполненными параллельно, обеспечивали бы следующий эффект:
-- первый запрос должен считать количество выданных на руки и возвращённых в библиотеку книг и не зависеть от запросов на обновление таблицы «subscriptions» (не ждать их завершения);
-- второй запрос должен инвертировать значения поля «sb_is_active» таблицы subscriptions с «Y» на «N» и наоборот и не зависеть от первого запроса (не ждать его завершения).

ALTER DATABASE [library] SET ALLOW_SNAPSHOT_ISOLATION ON;

SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
BEGIN TRANSACTION;
	DECLARE @ISSUED INT;
	DECLARE @RETURNED INT;
	SELECT @ISSUED = COUNT(*) FROM subscriptions WHERE sb_is_active = 'Y';
	SELECT @RETURNED = COUNT(*) FROM subscriptions WHERE sb_is_active = 'N';
	SELECT @ISSUED AS issued, @RETURNED AS returned;
COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
BEGIN TRANSACTION;
	UPDATE subscriptions SET sb_is_active = CASE sb_is_active WHEN 'Y' THEN 'N' ELSE 'Y' END;
COMMIT TRANSACTION;


-- Написать код, в котором запрос, инвертирующий значения поля «sb_is_active» таблицы «subscriptions» с «Y» на «N» и наоборот,
-- будет иметь максимальные шансы на успешное завершение в случае возникновения ситуации взаимной блокировки с другими транзакциями.

SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
BEGIN TRANSACTION;
	UPDATE subscriptions SET sb_is_active = CASE sb_is_active WHEN 'Y' THEN 'N' ELSE 'Y' END;
COMMIT TRANSACTION;

--Создать хранимую функцию, порождающую исключительную ситуацию в случае, если выполняются оба условия (подсказка: эта задача имеет решение только для MS SQL Server):
-- режим автоподтверждения транзакций выключен;
-- функция запущена из вложенной транзакции.

CREATE FUNCTION CHECK_TRANSACTION_LEVEL()
RETURNS INT
AS
BEGIN
	DECLARE @IMPLICIT_TRANSACTIONS VARCHAR(3) = 'OFF';
	IF ( (2 & @@OPTIONS) = 2 ) SET @IMPLICIT_TRANSACTIONS = 'ON';
	IF @@TRANCOUNT > 1 AND @IMPLICIT_TRANSACTIONS = 'ON' BEGIN
		RETURN 1;
	END
	RETURN 0;
END;