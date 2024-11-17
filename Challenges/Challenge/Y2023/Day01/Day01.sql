USE AoC;

SET NOCOUNT ON



IF NOT EXISTS (
                  SELECT    *
                  FROM      [INFORMATION_SCHEMA].[SCHEMATA] AS [S]
                  WHERE     [S].[SCHEMA_NAME] = 'AoC23'
              )
BEGIN
    EXEC [sys].[sp_executesql] N'CREATE SCHEMA AoC23';
END;
DROP TABLE IF EXISTS [AoC23].[AoC_Dec01];
CREATE TABLE [AoC23].[AoC_Dec01]
(
    [ID] int IDENTITY,
    [input] nvarchar(MAX) NULL
);
GO
DROP VIEW IF EXISTS [AoC23].[Dec01Bulk];
GO
CREATE VIEW [AoC23].[Dec01Bulk]
AS
SELECT  [input]
FROM    [AoC23].[AoC_Dec01];
GO
DECLARE @Path nvarchar(MAX) = N'K:\Development\AoC';
DECLARE @BulkSQL nvarchar(1000);
SET @BulkSQL = N'BULK INSERT AoC23.Dec01Bulk
FROM ''' + @Path + N'\Challenges\Challenge\Y2023\Day01\data.input''
WITH (
		 FIELDTERMINATOR = '','',
         ROWTERMINATOR = ''' + CHAR(13) + CHAR(10) + N'''
     );';
EXEC [sys].[sp_executesql] @BulkSQL;

DECLARE @Start datetime2(7) = GETDATE()

SELECT  *
FROM    [AoC23].[AoC_Dec01] AS [ACD];

DECLARE @CalibTable table
(
    [inputId] int,
    [calibration] int
);

INSERT INTO @CalibTable
(
    [calibration]
)
SELECT  SUBSTRING([ACD].[input], PATINDEX('%[0-9]%', [ACD].[input]), 1) + SUBSTRING(REVERSE([ACD].[input]), PATINDEX('%[0-9]%', REVERSE([ACD].[input])), 1)
FROM    [AoC23].[AoC_Dec01] AS [ACD];

SELECT  SUM([CT].[calibration])
FROM    @CalibTable AS [CT];

DELETE  FROM @CalibTable;

DECLARE @CalbWordTable table
(
    [inputId] int,
    [calibrationDigit] varchar(5),
    [first] int,
    [last] int
);

DECLARE @Digits table
(
    [digit] nvarchar(10),
    [valueNum] int
);

INSERT INTO @Digits
(
    [digit],
    [valueNum]
)
VALUES
(
    '%1%',
    1
),
(
    '%2%',
    2
),
(
    '%3%',
    3
),
(
    '%4%',
    4
),
(
    '%5%',
    5
),
(
    '%6%',
    6
),
(
    '%7%',
    7
),
(
    '%8%',
    8
),
(
    '%9%',
    9
),
(
    '%one%',
    1
),
(
    '%two%',
    2
),
(
    '%three%',
    3
),
(
    '%four%',
    4
),
(
    '%five%',
    5
),
(
    '%six%',
    6
),
(
    '%seven%',
    7
),
(
    '%eight%',
    8
),
(
    '%nine%',
    9
);



INSERT INTO @CalbWordTable
(
    [inputId],
    [calibrationDigit],
    [first],
    [last]
)
SELECT  [ACD].[ID],
        [D].[valueNum],
        PATINDEX([D].[digit], [ACD].[input]),
        LEN([ACD].[input]) - PATINDEX(REVERSE([D].[digit]), REVERSE([ACD].[input]))
FROM    [AoC23].[AoC_Dec01] AS [ACD]
        CROSS APPLY @Digits AS [D]
WHERE   PATINDEX([D].[digit], [ACD].[input]) <> 0;



WITH
[CTE] AS
(
    SELECT  [CWT].[inputId],
            FIRST_VALUE([CWT].[calibrationDigit]) OVER (PARTITION BY [inputId] ORDER BY [CWT].[first]) + FIRST_VALUE([CWT].[calibrationDigit]) OVER (PARTITION BY [inputId]  ORDER BY [CWT].[last] DESC) AS calibration
    FROM    @CalbWordTable AS [CWT]
)
INSERT INTO @CalibTable
(
    [inputId],
    [calibration]
)
SELECT      [CTE].[inputId],
            [CTE].calibration
FROM        [CTE]
GROUP BY    [CTE].[inputId],
            [CTE].calibration

SELECT  SUM([CT].[calibration])
FROM    @CalibTable AS [CT];


SELECT DATEDIFF(MILLISECOND, @Start, GETDATE())
