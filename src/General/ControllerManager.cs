using General.config;
using BuisnessLogic.controllers_inner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General
{
    public class ControllerManager
    {
        private CategoryController categoryController;
        public CategoryController CategoryController { get { return categoryController; } }

        private ContractController contractController;
        public ContractController ContractController { get { return contractController; } }

        private ContractPosController contractPosController;
        public ContractPosController ContractPosController { get { return contractPosController; } }

        private FirmController firmController;
        public FirmController FirmController { get { return firmController; } }

        private ProductController productController;
        public ProductController ProductController { get { return productController; } }

        private UserController userController;
        public UserController UserController { get { return userController; } }

        private WarehouseController warehouseController;
        public WarehouseController WarehouseController { get { return warehouseController; } }

        private WareProdController wareProdController;
        public WareProdController WareProdController { get { return wareProdController; } }

        private ProducerController producerController;
        public ProducerController ProducerController { get { return producerController; } }

        public ControllerManager(IConfig cfg)
        {
            categoryController = new CategoryController(cfg.GetProductCategoryRepository());
            contractController = new ContractController(cfg.GetContractRepository());
            contractPosController = new ContractPosController(cfg.GetContractPosRepository());
            firmController = new FirmController(cfg.GetFirmRepository());
            productController = new ProductController(cfg.GetProductRepository());
            userController = new UserController(cfg.GetUserRepository());
            warehouseController = new WarehouseController(cfg.GetWarehouseRepository());
            wareProdController = new WareProdController(cfg.GetWareProdRepository());
            producerController = new ProducerController(cfg.GetProducerRepository());
        }
    }
}
