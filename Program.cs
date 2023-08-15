using MySqlConnector;

string totalThreadsString;
string totalRowsPerThreadString;

int threadCount;
int rowsPerThread;

Console.WriteLine("IP Address");
string ipAddress = Console.ReadLine();

Console.WriteLine("Port");
string port = Console.ReadLine();

Console.WriteLine("Username");
string username = Console.ReadLine();

Console.WriteLine("Password");
string password = Console.ReadLine();

Console.WriteLine("Database name: ");
string database = Console.ReadLine();

Console.WriteLine("Total threads to use: ");
totalThreadsString = Console.ReadLine();

Console.WriteLine("Total rows to create per thread: ");
totalRowsPerThreadString = Console.ReadLine();


// convert to integer
threadCount = Convert.ToInt32(totalThreadsString);
rowsPerThread = Convert.ToInt32(totalRowsPerThreadString);

var startTime = DateTime.Now;

for (int i = 0; i < threadCount; i++)
{
    Thread thread = new Thread(InsertData);
    thread.Start();
}

void InsertData(){
    string connectionStr = String.Format("Server={0};port={1};username={2};password={3};Database={4}", ipAddress, port, username, password, database);
    MySqlConnection connection = new MySqlConnection(connectionStr);
    connection.Open();
    Random rand = new Random();
    
    for (int i = 0; i < rowsPerThread; i++)
    {
        string sqlInsert = String.Format("INSERT INTO test_table (col1, col2) VALUES ({0}, {1})", i.ToString(), rand.Next(0, 10000).ToString());
        MySqlCommand cmdInsert = new MySqlCommand(sqlInsert, connection);
        cmdInsert.ExecuteNonQuery();
    }
    
    connection.Close();
}

var endTime = DateTime.Now;
var timeSpan = endTime - startTime;
Console.WriteLine("Time taken: " + timeSpan.Minutes.ToString() +  ":" + timeSpan.Seconds.ToString() + ":" + timeSpan.Milliseconds.ToString());
Console.ReadKey();