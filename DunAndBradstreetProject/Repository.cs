using Dapper;
using DunAndBradstreetProject.Models;
using System.Data;
using System.Data.SqlClient;

namespace DunAndBradstreetProject
{
    // I've used interface for unit and integration tests.
    public interface IRepository
    {
        string GetAgentWithHighestAdvancedAmount(int year);

        List<Order> GetAgentsLatestOrder(List<string> agentCodes, int num);

        List<Agent> GetAgentsByOrderAmuont(int ordersCount, int year);
    }
    public class Repository : IRepository
    {
        private string _connectionString;

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }


        // First Question
        public string GetAgentWithHighestAdvancedAmount(int year)
        {
            string query = "stp_GetAgentWithHighestAdvancedAmount";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Year", year);

            using (SqlConnection con = OpenConnection())
            {
                string agentCode = con.Query<string>(query, parameters, commandType: CommandType.StoredProcedure).First();
                return agentCode;
            }
        }

        // Second Question
        public List<Order> GetAgentsLatestOrder(List<string> agentCodes, int num)
        {
            string query = "stp_GetAgentsLatestOrder";

            DynamicParameters parameters = new DynamicParameters();            
            parameters.Add("AgentCodes", CreateCodeIdsTable(agentCodes).AsTableValuedParameter("AgentCodeIds"));

            List<Order>? orders = null;

            using (SqlConnection con = OpenConnection())
            {
                 orders = con.Query<Order>(query, parameters, commandType: CommandType.StoredProcedure).ToList();              
            }

            if (orders == null)
                return new List<Order>();

            Dictionary<string, Order> agentsOrder = new Dictionary<string, Order>();

            List<string> agents = orders.GroupBy(o => o.AgentCode).Select(o => o.Key).ToList();

            foreach (string code in agents)
            {
                if (!agentsOrder.Keys.Contains(code))
                {
                    List<Order> agentOrders = orders.Where(o => o.AgentCode == code).ToList();
                    if (num >= agentOrders.Count())
                    {
                        agentsOrder.Add(code, agentOrders[agentOrders.Count() - 1]);
                    }
                    else
                    {
                        agentsOrder.Add(code, agentOrders[num - 1]);
                    }
                }
            }

            return agentsOrder.Values.ToList(); 
            // The order object contains the agent code. 
            // I though about returning the dictionary agentsOrder, Its also a posibility. 
        }

        private DataTable CreateCodeIdsTable(List<string> agentCodes)
        {
            DataTable codes = new DataTable();
            codes.Columns.Add("Code");

            foreach (string code in agentCodes)
            {
                DataRow newRow = codes.NewRow();
                newRow["Code"] = code;
                codes.Rows.Add(newRow);
            }

            return codes;
        }

        // Third Question
        public List<Agent> GetAgentsByOrderAmuont(int ordersCount, int year)
        {
            string query = "stp_GetAgentsByOrderAmuont";

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Year", year);
            parameters.Add("OrdersCount", ordersCount);
          
            using (SqlConnection con = OpenConnection())
            {
                List<Agent> agents = con.Query<Agent>(query, parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
                return agents;
            }
        }

        private SqlConnection OpenConnection()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            return con;
        }

      
    }
}
