using General.config;
using General.db_connect;
using ResellerShop.ui;
using System.Xml.Linq;

Logger.Logger.SetLogfile("log.txt");
Logger.Logger.Info("Begins session");
IConfigCreator cfgCreator = new PostgresConfigCreator();
IConfig cfg = cfgCreator.Create("config.json");
ConsoleApp app = new(cfg);
app.Mainloop();
