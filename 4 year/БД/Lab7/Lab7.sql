-- ������� �������� ���������, �������:
-- * ��������� ������ ����� ��� ��������� �����;
-- * �������� ����������� ��������, ���� � �������� ������ ���� �� ���� �������� ������� ����������� ������� � ���� ������������ �������� ���������� ����� ������� �m2m_books_genres� (�.�. � ����� ����� ��� ��� ����� ����).

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

-- ������� �������� ���������, �������:
-- ����������� �������� ���� �b_quantity� ��� ���� ���� � ��� ����;
-- �������� ����������� ��������, ���� �� ����� ���������� �������� ������� ���������� ����������� ���� �������� �������� 50

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

-- �������� �������, �������, ������ ������������ �����������, ������������ �� ��������� ������:
-- ������ ������ ������ ������� ���������� �������� �� ���� � ������������ � ���������� ���� � �� �������� �� �������� �� ���������� ������� �subscriptions� (�� ����� �� ����������);
-- ������ ������ ������ ������������� �������� ���� �sb_is_active� ������� subscriptions � �Y� �� �N� � �������� � �� �������� �� ������� ������� (�� ����� ��� ����������).

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


-- �������� ���, � ������� ������, ������������� �������� ���� �sb_is_active� ������� �subscriptions� � �Y� �� �N� � ��������,
-- ����� ����� ������������ ����� �� �������� ���������� � ������ ������������� �������� �������� ���������� � ������� ������������.

SET TRANSACTION ISOLATION LEVEL SNAPSHOT;
BEGIN TRANSACTION;
	UPDATE subscriptions SET sb_is_active = CASE sb_is_active WHEN 'Y' THEN 'N' ELSE 'Y' END;
COMMIT TRANSACTION;

--������� �������� �������, ����������� �������������� �������� � ������, ���� ����������� ��� ������� (���������: ��� ������ ����� ������� ������ ��� MS SQL Server):
-- ����� ����������������� ���������� ��������;
-- ������� �������� �� ��������� ����������.

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