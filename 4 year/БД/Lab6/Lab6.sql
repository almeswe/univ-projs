-- —оздать хранимую функцию, получающую на вход идентификатор читател€ и возвращающую список идентификаторов книг, 
-- которые он уже прочитал и вернул в библиотеку.

CREATE PROCEDURE GET_READ_BOOKS (
	@S_ID INT
) 
AS 
BEGIN
	SELECT sb_book FROM subscriptions WHERE 
		@S_ID = sb_subscriber AND
		subscriptions.sb_is_active = 'N';
END;

--EXEC GET_READ_BOOKS @S_ID = 4;

-- —оздать хранимую функцию, получающую на вход идентификатор читател€ и возвращающую 1, 
-- если у читател€ на руках сейчас менее дес€ти книг, и 0 в противном случае

CREATE PROCEDURE MORE_THAN_1O_BOOKS (
	@S_ID INT,
	@FACT TINYINT OUTPUT
)
AS
BEGIN
	DECLARE @COUNT_OF_BOOKS AS INT;
	SELECT @COUNT_OF_BOOKS = COUNT(*) FROM subscriptions 
		WHERE subscriptions.sb_id = @S_ID AND subscriptions.sb_is_active = 'Y'
		GROUP BY subscriptions.sb_id;
	IF @COUNT_OF_BOOKS < 10
		SET @FACT = 1; 
	ELSE
		SET @FACT = 0;
END;

--DECLARE @RES AS INT ;
--EXEC MORE_THAN_1O_BOOKS @S_ID = 3, @FACT = @RES OUTPUT;
--PRINT @RES;

-- Cоздать хранимую функцию, получающую на вход год издани€ книги и возвращающую 1,
-- если книга издана менее ста лет назад, и 0 в противном случае.

CREATE PROCEDURE MORE_THAN_100_YEARS (
	@UPLOAD_YEAR INT,
	@FACT_MORE TINYINT OUTPUT
)
AS
BEGIN
	DECLARE @CURR_YEAR AS INT;
	SELECT @CURR_YEAR = YEAR(GETDATE());
	IF ABS(@UPLOAD_YEAR - @CURR_YEAR) < 100
		SET @FACT_MORE = 1;
	ELSE
		SET @FACT_MORE = 0;
END;

--DECLARE @RES AS INT ;
--EXEC MORE_THAN_100_YEARS @UPLOAD_YEAR = 2000, @FACT_MORE = @RES OUTPUT;
--PRINT @RES;

-- —оздать хранимую процедуру, автоматически создающую и наполн€ющую данными таблицу Ђarrearsї, в которой должны быть представлены идентификаторы и имена читателей, у 
-- которых до сих пор находитс€ на руках хот€ бы одна книга, по которой дата возврата установлена в прошлом относительно текущей даты.

CREATE PROCEDURE GENERATE_ARREARS 
AS
BEGIN
	IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'arrears')
		CREATE TABLE arrears (
			a_id INT IDENTITY(1,1) PRIMARY KEY,
			a_sb_id INT,
			a_sb_name VARCHAR(150)
		);
	WITH TEMP AS (
		SELECT sb_subscriber, COUNT(*) AS sb_count FROM subscriptions
			WHERE sb_is_active = 'N' AND DAY(sb_finish) - DAY(GETDATE()) < 0
			GROUP BY sb_subscriber
	)
	INSERT INTO arrears (a_sb_id, a_sb_name) 
		SELECT sb_subscriber, s_name AS sb_name FROM TEMP 
			JOIN subscribers ON sb_subscriber = s_id;
END;

--EXEC GENERATE_ARREARS;
--SELECT * FROM arrears;

CREATE PROCEDURE DELETE_VIEWS
AS
BEGIN
	SELECT * FROM sys.views;
END;

select count(1) from task_1;

SELECT * from subscribers;
SELECT * from subscriptions;