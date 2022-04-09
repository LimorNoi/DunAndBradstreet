ALTER PROCEDURE stp_GetAgentsLatestOrder
(
	@AgentCodes AgentCodeIds READONLY 
)
AS


SELECT    AGENTS.AGENT_CODE AS AgentCode,
		  ORD_NUM			AS Num,
		  ORD_AMOUNT		AS Amount,
		  ADVANCE_AMOUNT	AS AdvanceAmount,
		  ORD_DATE			AS [Date],
		  CUST_CODE			AS CustCode,
		  ORD_DESCRIPTION	AS [Description]
FROM AGENTS
JOIN ORDERS ON AGENTS.AGENT_CODE= ORDERS.AGENT_CODE
JOIN @AgentCodes ON AGENTS.AGENT_CODE = Code
ORDER BY AGENTS.AGENT_CODE, ORD_DATE