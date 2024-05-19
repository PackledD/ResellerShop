using General.config;
using General.db_connect;

string sql = "";
sql += "CREATE TABLE IF NOT EXISTS Firms(" +
    "id INT PRIMARY KEY," +
    "name VARCHAR(128) NOT NULL," +
    "phone VARCHAR(32) NOT NULL," +
    "email VARCHAR(128) NOT NULL," +
    "physical_addr VARCHAR(128) NOT NULL," +
    "legal_addr VARCHAR(128) NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS ProductCategories(" +
    "id INT PRIMARY KEY," +
    "name VARCHAR(128) NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS Producers(" +
    "id INT PRIMARY KEY," +
    "name VARCHAR(128) NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS Products(" +
    "id INT PRIMARY KEY," +
    "name VARCHAR(128) NOT NULL," +
    "category INT NOT NULL," +
    "provider INT NOT NULL," +
    "cost INT NOT NULL," +
    "producer INT NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS Users(" +
    "id INT PRIMARY KEY," +
    "fullname VARCHAR(128) NOT NULL," +
    "firm INT NOT NULL," +
    "email VARCHAR(128) UNIQUE NOT NULL," +
    "phone VARCHAR(32) NOT NULL," +
    "kind INT NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS Warehouses(" +
    "id INT PRIMARY KEY," +
    "addr VARCHAR(128) NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS Contracts(" +
    "id INT PRIMARY KEY," +
    "firm_id INT NOT NULL," +
    "director1_id INT," +
    "director2_id INT," +
    "manager1_id INT," +
    "manager2_id INT," +
    "conclusion_date DATE NOT NULL," +
    "expiration_date DATE NOT NULL," +
    "document VARCHAR(128) NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS ContractPositions(" +
    "contract_id INT NOT NULL," +
    "product_id INT NOT NULL," +
    "warehouse_id INT NOT NULL," +
    "count INT NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS ProductsInWarehouses(" +
    "product_id INT NOT NULL," +
    "warehouse_id INT NOT NULL," +
    "count INT NOT NULL" +
    ");";
sql += "CREATE TABLE IF NOT EXISTS AuthData(" +
    "user_id INT NOT NULL," +
    "password_hash VARCHAR(64) NOT NULL" +
    ");";

sql += "ALTER TABLE IF EXISTS Products " +
    "ADD CONSTRAINT fk_products_cat FOREIGN KEY (category) REFERENCES ProductCategories(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_products_prod FOREIGN KEY (producer) REFERENCES Producers(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_products_prov FOREIGN KEY (provider) REFERENCES Firms(id) ON DELETE CASCADE;";
sql += "ALTER TABLE IF EXISTS Users " +
    "ADD CONSTRAINT fk_users_firm FOREIGN KEY (firm) REFERENCES Firms(id) ON DELETE CASCADE;";
sql += "ALTER TABLE IF EXISTS Contracts " +
    "ADD CONSTRAINT fk_contracts_u1 FOREIGN KEY (director1_id) REFERENCES Users(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_contracts_u2 FOREIGN KEY (director2_id) REFERENCES Users(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_contracts_u3 FOREIGN KEY (manager1_id) REFERENCES Users(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_contracts_u4 FOREIGN KEY (manager2_id) REFERENCES Users(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_contracts_firm FOREIGN KEY (firm_id) REFERENCES Firms(id) ON DELETE CASCADE;";
sql += "ALTER TABLE IF EXISTS ContractPositions " +
    "ADD CONSTRAINT fk_contr_pos_contract FOREIGN KEY (contract_id) REFERENCES Contracts(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_contr_pos_product FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_contr_pos_warehouse FOREIGN KEY (warehouse_id) REFERENCES Warehouses(id) ON DELETE CASCADE;";
sql += "ALTER TABLE IF EXISTS ProductsInWarehouses " +
    "ADD CONSTRAINT fk_ware_prod_product FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE CASCADE," +
    "ADD CONSTRAINT fk_ware_prod_warehouse FOREIGN KEY (warehouse_id) REFERENCES Warehouses(id) ON DELETE CASCADE;";
sql += "ALTER TABLE IF EXISTS AuthData " +
    "ADD CONSTRAINT fk_auth_user FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE;";

sql += "INSERT INTO Firms VALUES " +
    "(0, 'Reseller Shop', '1234567890', 'firm@firm.org', 'Phys addr', 'Legal addr');";
IConfigCreator cfgCreator = new PostgresConfigCreator();
IConfig cfg = cfgCreator.Create("config.json");
IDbConnector con = PostgresDbConnectorCreator.Create(); ;
con.Request(sql).ExecNonQuery();
con.Close();
