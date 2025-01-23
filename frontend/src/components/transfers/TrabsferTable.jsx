import { motion } from "framer-motion";
import { Search } from "lucide-react";
import { useState, useEffect, useRef } from "react";
import { useProducts } from "../../ProductProvider";
import {useOrder} from "../../OrderProvider";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';



const TransferTable = ({type}) => {
    const transfers = [
        {
          StockMovementId: 1,
          ProductId: 101,
          Product: {
            ProductId: 101,
            ProductName: "Laptop"
          },
          WarehouseId: 201,
          Warehouse: {
            WarehouseId: 201,
            WarehouseName: "Main Warehouse"
          },
          LocationId: 301,
          Location: {
            LocationId: 301,
            LocationName: "Aisle 3, Shelf B"
          },
          OrderId: 401,
          Order: {
            OrderId: 401,
            OrderNumber: "ORD12345",
          },
          Quantity: 15,
          MovementType: "IN",
          MovementDate: "2023-10-01T12:00:00Z"
        },
        {
          StockMovementId: 2,
          ProductId: 102,
          Product: {
            ProductId: 102,
            ProductName: "Smartphone"
          },
          WarehouseId: 202,
          Warehouse: {
            WarehouseId: 202,
            WarehouseName: "Secondary Warehouse"
          },
          LocationId: 302,
          Location: {
            LocationId: 302,
            LocationName: "Aisle 1, Shelf A"
          },
          OrderId: 402,
          Order: {
            OrderId: 402,
            OrderNumber: "ORD67890",
            status: "Delivered"
          },
          Quantity: 30,
          MovementType: "OUT",
          MovementDate: "2023-10-02T12:00:00Z"
        }
      ];
  const delivered = transfers.filter((transfer)=>transfer.MovementType==="OUT" && transfer.Order.status==="Delivered");
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredOrders, setFilteredOrders] = useState([]);
  const imported = transfers.filter((transfer)=>transfer.MovementType==="IN");

  const [searchImportTerm, setSearchImportTerm] = useState("");
  const [filteredImports, setFilteredImports] = useState([]);



//Notification


  //console.log(selectedList);
  //console.log();
useEffect(()=>{
  const filtred = delivered.filter(
    (transfer)=>transfer.Product.ProductName.toLowerCase().includes(searchTerm)
    /* nzid filter 3l category */
  )
  setFilteredOrders(filtred);
},[searchTerm,delivered])

useEffect(()=>{
    const filtred = imported.filter(
      (transfer)=>transfer.Product.ProductName.toLowerCase().includes(searchImportTerm)
      /* nzid filter 3l category */
    )
    setFilteredImports(filtred);
  },[searchImportTerm,imported])
  

const handleSearchOrder = (e) => {
  const term = e.target.value.toLowerCase();
  setSearchTerm(term);
  
};

const handleSearchImports = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchImportTerm(term);
}

  if(!delivered){
    return <div>Loading...</div>
  }

  return (
    type =="OUT" ? <motion.div
      className="ml-14 mt-4 bg-gradient-to-br w-10/12 from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
      initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.4 }}
    >
      <div className="flex justify-between items-center mb-6">
        <div className="ml-auto relative">
          <input
            type="text"
            placeholder="Search products..."
            className="bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            onChange={handleSearchOrder}
            value={searchTerm}
          />
          <Search className="absolute left-3 top-2.5 text-gray-400" size={18} />
        </div>
      </div>

      <div className="overflow-x-auto">
  <table className="min-w-full border-spacing-y-4">
    <thead>
      <tr className="text-gray-100">
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Name</th>
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Warehouse</th>
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Location</th>
      </tr>
    </thead>
    <tbody>
      {filteredOrders.map((order) => (
        <motion.tr
          key={order.StockMovementId}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.3 }}
          className="text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
        >
          <td className="flex items-center px-6 py-4 text-sm">
            <img
              src="https://images.unsplash.com/photo-1627989580309-bfaf3e58af6f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8d2lyZWxlc3MlMjBlYXJidWRzfGVufDB8fDB8fHww"
              alt="Product img"
              className="w-10 h-10 rounded-full mr-4"
            />
            {order.Product.ProductName}
          </td>
          <td className="px-6 py-4 text-sm">{order.Quantity}</td>
          <td className="px-6 py-4 text-sm">{order.Warehouse.WarehouseName}</td>
          <td className="px-6 py-4 text-sm">{order.Location.LocationName}</td>
        </motion.tr>
      ))}
    </tbody>
  </table>
</div>
    </motion.div>
    :
    <motion.div
      className="ml-14 mt-4 bg-gradient-to-br w-10/12 from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
      initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.4 }}
    >
      <div className="flex justify-between items-center mb-6">
        <div className="ml-auto relative">
          <input
            type="text"
            placeholder="Search products..."
            className="bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            onChange={handleSearchImports}
            value={searchImportTerm}
          />
          <Search className="absolute left-3 top-2.5 text-gray-400" size={18} />
        </div>
      </div>

      <div className="overflow-x-auto">
  <table className="min-w-full border-spacing-y-4">
    <thead>
      <tr className="text-gray-100">
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Name</th>
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Warehouse</th>
        <th className="px-6 py-3 text-left text-sm font-medium uppercase">Location</th>
      </tr>
    </thead>
    <tbody>
      {filteredImports.map((order) => (
        <motion.tr
          key={order.StockMovementId}
          initial={{ opacity: 0 }}
          animate={{ opacity: 1 }}
          transition={{ duration: 0.3 }}
          className="text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
        >
          <td className="flex items-center px-6 py-4 text-sm">
            <img
              src="https://images.unsplash.com/photo-1627989580309-bfaf3e58af6f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8d2lyZWxlc3MlMjBlYXJidWRzfGVufDB8fDB8fHww"
              alt="Product img"
              className="w-10 h-10 rounded-full mr-4"
            />
            {order.Product.ProductName}
          </td>
          <td className="px-6 py-4 text-sm">{order.Quantity}</td>
          <td className="px-6 py-4 text-sm">{order.Warehouse.WarehouseName}</td>
          <td className="px-6 py-4 text-sm">{order.Location.LocationName}</td>
        </motion.tr>
      ))}
    </tbody>
  </table>
</div>
    </motion.div>
  );
};

export default TransferTable;
