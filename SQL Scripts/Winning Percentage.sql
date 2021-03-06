DECLARE @buys FLOAT, @wins FLOAT

SET @buys = (SELECT COUNT(*)
FROM Drinks.dbo.Transactions
WHERE Amount < 0)

SET @wins = (SELECT COUNT(*)
FROM Drinks.dbo.Transactions
WHERE ExecutorUserId = -1)

SELECT Name, Wins, Buys, ROUND (CAST(Wins AS FLOAT) / CAST(Buys AS FLOAT) * 100, 2) as Percentage
FROM
	(SELECT Id, Name, Wins, Buys
	FROM Drinks.dbo.Users JOIN
			(SELECT UserId, COUNT(*) as Wins
			FROM Drinks.dbo.Transactions
			WHERE ExecutorUserId = -1
			GROUP BY UserId) AS Wins
		ON Users.Id = Wins.UserId JOIN
			(SELECT ExecutorUserId, COUNT(*) as Buys
			FROM Drinks.dbo.Transactions
			WHERE Amount < 0
			GROUP BY ExecutorUserId) AS Buys
		ON Users.Id = Buys.ExecutorUserId) as Stats
UNION
SELECT Name, 0 as Wins, Buys, 0 as Percentage
FROM Drinks.dbo.Users INNER JOIN
		(SELECT UserId, COUNT(*) as Buys
		FROM Drinks.dbo.Transactions
		GROUP BY UserId) AS TransactionCount
	ON Users.Id = TransactionCount.UserId
WHERE Id NOT IN
		(SELECT UserId
		FROM Drinks.dbo.Transactions
		WHERE ExecutorUserId = -1)
	AND Id <> -1
UNION
SELECT 'TOTAL', @buys, @wins, ROUND(@wins / @buys * 100, 2)
ORDER BY Percentage DESC, Buys ASC