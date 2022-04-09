USE [DunAndBradstreet]
GO

/****** Object:  StoredProcedure [dbo].[stp_GetAgentsByOrderAmuont]    Script Date: 09/04/2022 19:09:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[stp_GetAgentsByOrderAmuont]
(
	@Year INT,
	@OrdersCount INT
)
AS


SELECT AGENTS.AGENT_CODE AS Code,
	   AGENT_Name		 AS Name, 
	   PHONE_NO			 AS PhoneNumber
FROM AGENTS
JOIN ORDERS ON AGENTS.AGENT_CODE= ORDERS.AGENT_CODE
WHERE DATEPART(YEAR, ORD_DATE) = @Year
GROUP BY AGENTS.AGENT_CODE, AGENT_Name, PHONE_NO
HAVING SUM(ADVANCE_AMOUNT) >= @OrdersCount
	
GO


