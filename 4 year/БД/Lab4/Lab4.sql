-- Добавить в базу данных информацию о троих новых читателях: «Орлов О.О.», «Соколов С.С.», «Беркутов Б.Б.».
INSERT INTO subscribers (s_name) VALUES ('Орлов О.О.');
INSERT INTO subscribers (s_name) VALUES ('Соколов С.С.');
INSERT INTO subscribers (s_name) VALUES ('Беркутов Б.Б.');

--Удалить все книги, относящиеся к жанру «Классика». 
WITH [TEMP] AS (
	SELECT [books].[b_id], [m2m_books_genres].[g_id] FROM books 
		JOIN m2m_books_genres ON [books].[b_id] = [m2m_books_genres].[b_id]
)
DELETE [books] FROM [TEMP] 
	WHERE [TEMP].[b_id] = [books].[b_id] AND [TEMP].[g_id] = 5;

--Добавить в базу данных жанры «Политика», «Психология», «История».
IF NOT EXISTS (SELECT * FROM genres WHERE g_name = 'Политика')
	INSERT INTO genres (g_name) VALUES ('Политика');
IF NOT EXISTS (SELECT * FROM genres WHERE g_name = 'Психология')
	INSERT INTO genres (g_name) VALUES ('Психология');
IF NOT EXISTS (SELECT * FROM genres WHERE g_name = 'История')
	INSERT INTO genres (g_name) VALUES ('История');

--Добавить в базу данных читателей с именами «Сидоров С.С.», «Иванов И.И.», «Орлов О.О.»; если читатель с таким именем уже существует, 
--добавить в конец имени нового читателя порядковый номер в квадратных скобках (например, если при добавлении читателя «Сидоров С.С.» 
--выяснится, что в базе данных уже есть четыре таких читателя, имя добавляемого должно превратиться в «Сидоров С.С. [5]»).
CREATE PROCEDURE ADD_DUPLICATED_SUBSCRIBER  
	@SUBSCRIBER_NAME NVARCHAR(150) AS
BEGIN
	DECLARE @COUNT_OF_USERS AS INTEGER = 0;
	IF EXISTS (SELECT s_name FROM subscribers WHERE s_name = @SUBSCRIBER_NAME)
		SELECT @COUNT_OF_USERS = COUNT(*) FROM (
			SELECT s_id, s_name FROM subscribers
				WHERE s_name = @SUBSCRIBER_NAME
		) AS TEMP
		GROUP BY (s_name);
	PRINT  @COUNT_OF_USERS;
	IF @COUNT_OF_USERS != 0
		SET @SUBSCRIBER_NAME = CONCAT(@SUBSCRIBER_NAME, ' [', @COUNT_OF_USERS, ']');
	INSERT INTO subscribers (s_name) VALUES (@SUBSCRIBER_NAME);
END;
EXEC ADD_DUPLICATED_SUBSCRIBER 'Сидоров С.С.';
EXEC ADD_DUPLICATED_SUBSCRIBER 'Иванов И.И.';
EXEC ADD_DUPLICATED_SUBSCRIBER 'Орлов О.О.';
DROP PROCEDURE ADD_DUPLICATED_SUBSCRIBER;

SELECT * FROM subscriptions;
--Отметить все выдачи с идентификаторами ≤50 как возвращённые
UPDATE [subscriptions] SET [sb_is_active] = 'N' WHERE sb_id >= 50;