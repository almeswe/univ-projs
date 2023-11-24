-- ������� �������������, ����������� �������� ������ ��������� � ����������� ����������� � ������� �������� �� ����� ����, 
-- �� ������������ ������ ����� ���������, �� ������� ������� �������������, �.�. �� ����� � �������� ���� ���� �� ���� �����, 
-- ������� �� ������ ��� ������� �� ����������� ������� ����.

CREATE VIEW task_1 AS
	WITH TEMP AS (
		SELECT DISTINCT sb_subscriber, sb_is_active FROM subscriptions
			WHERE sb_is_active = 'Y'
	)
	SELECT [subscriptions].[sb_subscriber], COUNT(sb_book) as sb_amount FROM subscriptions 
		JOIN TEMP ON [TEMP].[sb_subscriber] = [subscriptions].[sb_subscriber]
		GROUP BY [subscriptions].[sb_subscriber];

-- ������� �������������, ������������ ��� ���������� �� ������� subscriptions, 
-- ���������� ���� �� ����� sb_start � sb_finish � ������ �����-��-�� �ͻ, ��� ��ͻ � ���� ������ � ���� ������ ������� �������� 
-- (�.�. ������������, �������� � �.�.)

CREATE VIEW task_2 AS
	SELECT sb_id, sb_subscriber, sb_book, 
			CONCAT(sb_start, ' ', DATENAME(dw, sb_start)) AS sb_start, 
			CONCAT(sb_finish, ' ', DATENAME(dw, sb_finish)) AS sb_finish, 
			sb_is_active 
		FROM subscriptions;
SELECT * FROM task_2;

-- ������� �������, ����������� ����������� � ���������� ������ ����� �������, ��� ������� �� �������� ������� �������� ����� 
-- ����, ����, ������ - (�����), ' (��������) � �������� (�� ����������� ��� � ����� ������ ������ �������).

DROP TRIGGER IF EXISTS after_insert_authors_task3;
CREATE TRIGGER after_insert_authors_task3
	ON authors AFTER INSERT AS
BEGIN
	DECLARE @NAME AS VARCHAR(150);
	SELECT @NAME = a_name FROM inserted;
	IF CHARINDEX(@NAME, '  ') > 0 BEGIN
		RAISERROR('Invalid author name.', 16, 1);
		ROLLBACK TRANSACTION;
	END;
	IF @NAME NOT LIKE '%[a-zA-Z0-9'' -]%' BEGIN
		RAISERROR('Invalid author name.', 16, 1);
		ROLLBACK TRANSACTION;
	END;
END

-- test cases
INSERT INTO authors (a_name) VALUES ('Some name');

-- ������� �������, �� ����������� �������� � ���� ������ ���������� � ������ �����, ���� ����������� ���� �� ���� �� �������:
--	���� ������ ��� �������� ���������� �� �����������;
--	�������� ���� �� ��������� ������� ����� 100 ����;
--	���������� ������� ����� ������ ������ � �������� ����� ��� ����.
DROP TRIGGER IF EXISTS after_insert_subscriptions_task4;
CREATE TRIGGER after_insert_subscriptions_task4
	ON subscriptions AFTER INSERT AS 
BEGIN
	DECLARE @ID AS INT;
	DECLARE @START AS DATETIME;
	DECLARE @FINISH AS DATETIME;
	SELECT @ID = sb_subscriber, @START = sb_start, @FINISH = sb_finish FROM inserted;
	IF DATEPART(WEEKDAY, @START) = 1 OR
	   DATEPART(WEEKDAY, @FINISH) = 1 BEGIN
	   RAISERROR('Cannot return or give book on sunday.', 16, 1);
       ROLLBACK TRANSACTION;
	END;
	IF DATEDIFF(DAY, @START, @FINISH) < 3 BEGIN
	   RAISERROR('Datediff of start and finish is less than 3 days.', 16, 1);
       ROLLBACK TRANSACTION;
	END;
	DECLARE @COUNT_OF_BOOKS AS INT;
	SELECT @COUNT_OF_BOOKS = COUNT(*) FROM (
		SELECT * FROM subscriptions
			WHERE @ID = sb_subscriber AND DATEDIFF(MONTH, GETDATE(), sb_start) <= 6
	) AS X GROUP BY sb_subscriber;
	IF @COUNT_OF_BOOKS > 100 BEGIN
		RAISERROR('This person took more than 100 books in last 6 months.', 16, 1);
		ROLLBACK TRANSACTION;
	END;
END

-- test cases

INSERT INTO subscriptions 
	(sb_subscriber, sb_book, sb_start, sb_finish, sb_is_active) 
	VALUES (1, 1, '2023-11-01', '2023-11-05', 'N');

INSERT INTO subscriptions 
	(sb_subscriber, sb_book, sb_start, sb_finish, sb_is_active) 
	VALUES (1, 1, '2023-11-03', '2023-11-04', 'N');

-- ������� �������, �������������� �������� ����� ����� �������, ����� ��� ������������� ��������� ��������:
--	�� ����������� ������� �������� � ������ � ����� ��������;
--	�� ����������� ������� ������������� ��������;
--	������ ����� � �������� ������ ������ ���� ���������

DROP TRIGGER IF EXISTS after_insert_books_task5;
CREATE TRIGGER after_insert_books_task5
	ON books AFTER INSERT AS
BEGIN
	DECLARE @ID AS INT;
	DECLARE @NAME AS VARCHAR(150);
	SELECT @ID = b_id, @NAME = b_name FROM inserted;
	SELECT @NAME = TRIM(' ' FROM  @NAME);
	WHILE CHARINDEX('  ', @NAME) > 0
		SET @NAME = REPLACE(@NAME, '  ', ' ');
	SELECT @NAME = CONCAT(UPPER(LEFT(@NAME, 1)), LOWER(SUBSTRING(@NAME, 2, LEN(@NAME))));
	UPDATE books SET b_name = @NAME WHERE b_id = @ID;
END

-- test case
INSERT INTO books (b_name, b_year, b_quantity)
	VALUES (' test    name    ', 2000, 1);
DELETE FROM books WHERE b_name = 'Test name';