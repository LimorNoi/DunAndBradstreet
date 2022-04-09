USE [DunAndBradstreet]
GO

/****** Object:  StoredProcedure [dbo].[stp_GetAgentWithHighestAdvancedAmount]    Script Date: 09/04/2022 11:06:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stp_GetAgentWithHighestAdvancedAmount]
(
	@Year INT 
)
AS

WITH AdvancedAmountPerAgent
AS
(
	SELECT AGENTS.AGENT_CODE,
		   SUM(ADVANCE_AMOUNT) AS AdvancedAmountSum
	FROM AGENTS
	JOIN ORDERS ON AGENTS.AGENT_CODE= ORDERS.AGENT_CODE
	WHERE DATEPART(YEAR, ORD_DATE) = @Year
	AND DATEPART(MONTH, ORD_DATE) >= 1 AND DATEPART(MONTH, ORD_DATE) <= 3 -- First quarter
	GROUP BY AGENTS.AGENT_CODE
)

SELECT TOP 1 AGENT_CODE 
FROM AdvancedAmountPerAgent
ORDER BY AdvancedAmountSum DESC 
GO


