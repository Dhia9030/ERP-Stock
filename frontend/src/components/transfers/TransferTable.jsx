import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import * as SignalR from '@microsoft/signalr';


const transfers = [
  {
    name: "Phone",
    quantity: 5,
    manufacturer: "Microsoft",
    orderId: "ORD001",
    orderDate: "2024-12-05",
    state: "IN",
    details: [
      { productItemId: 1, locationName: "Aisle1", wareHouse: "Main Warehouse", entryDate: "2024-11-05", supplier: "Achref Hemissi" },
      { productItemId: 2, locationName: "Aisle1", wareHouse: "Main Warehouse", entryDate: "2024-11-05", supplier: "Achref Hemissi" },
      { productItemId: 3, locationName: "Aisle1", wareHouse: "Main Warehouse", entryDate: "2024-11-05", supplier: "Achref Hemissi" },
      { productItemId: 4, locationName: "Aisle1", wareHouse: "Main Warehouse", entryDate: "2024-11-05", supplier: "Achref Hemissi" },
      { productItemId: 5, locationName: "Aisle1", wareHouse: "Main Warehouse", entryDate: "2024-11-05", supplier: "Achref Hemissi" }
    ]
  },
  {
    name: "Laptop",
    quantity: 10,
    manufacturer: "Apple",
    orderId: "ORD002",
    orderDate: "2024-12-10",
    state: "OUT",
    details: [
      { productItemId: 6, locationName: "Aisle2", wareHouse: "Main Warehouse", entryDate: "2024-11-10", supplier: "John Doe" },
      { productItemId: 7, locationName: "Aisle2", wareHouse: "Main Warehouse", entryDate: "2024-11-10", supplier: "John Doe" },
      { productItemId: 8, locationName: "Aisle2", wareHouse: "Main Warehouse", entryDate: "2024-11-10", supplier: "John Doe" },
      { productItemId: 9, locationName: "Aisle2", wareHouse: "Main Warehouse", entryDate: "2024-11-10", supplier: "John Doe" },
      { productItemId: 10, locationName: "Aisle2", wareHouse: "Main Warehouse", entryDate: "2024-11-10", supplier: "John Doe" }
    ]
  }
];

const TransferTable = ({type}) => {
  const [transfers, setTransfers] = useState([]);

  const [searchTerm, setSearchTerm] = useState("");
  const [filteredTransfers, setFilteredTransfers] = useState([]);
  const [expandedRow, setExpandedRow] = useState(null);



  useEffect(() => {
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/transferhub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub for transfers");

        connection.on("ReceiveTransfers", (data) => {
          console.log("Received transfers:", data);
          setTransfers(data);
        });

        connection.invoke("GetInitialTransfers")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
  }, []);



  useEffect(() => {
    const filtered = transfers.filter((transfer) =>
      transfer.name.toLowerCase().includes(searchTerm) ||
      transfer.manufacturer.toLowerCase().includes(searchTerm) ||
      transfer.orderId.toLowerCase().includes(searchTerm) ||
      transfer.orderDate.toLowerCase().includes(searchTerm) ||
      transfer.state.toLowerCase().includes(searchTerm)
    );
    setFilteredTransfers(filtered);
  }, [searchTerm]);

  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
  };

  const handleRowClick = (index) => {
    setExpandedRow(expandedRow === index ? null : index);
  };

  return (
    <motion.div
      className="ml-14 mt-2 w-11/12 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
    >
      <div className="relative">
      <input
        type="text"
        placeholder="Search..."
        value={searchTerm}
        onChange={handleSearch}
        className=" pl-12 mb-4 p-2 rounded-xl bg-gray-800"
      />
      <Search className='absolute left-3 top-2.5 text-gray-400' size={18} />
      </div>
      
      
      <div className="overflow-x-auto">
        <table className="min-w-full border-spacing-y-4">
          <thead>
            <tr className="text-gray-100">
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Name</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Manufacturer</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Order ID</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Order Date</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">State</th>
            </tr>
          </thead>
          <tbody>
            {filteredTransfers.map((transfer, index) => (
              transfer.state==type ? 
              <React.Fragment key={index}>
                <motion.tr
                  onClick={() => handleRowClick(index)}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ duration: 0.3 }}
                  className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
                >
                  <td className="flex items-center px-6 py-4 text-sm">
                    <img
                      src="https://images.unsplash.com/photo-1627989580309-bfaf3e58af6f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8d2lyZWxlc3MlMjBlYXJidWRzfGVufDB8fDB8fHww"
                      alt="Product img"
                      className="w-10 h-10 rounded-full mr-4"
                    />
                    <span className='font-bold text-lg'>{transfer.name}</span><span className='ml-3 text-xs'>click for details</span>
                  </td>
                  <td className="px-6 py-4 text-sm">{transfer.quantity}</td>
                  <td className="px-6 py-4 text-sm">{transfer.manufacturer}</td>
                  <td className="px-6 py-4 text-sm">{transfer.orderId}</td>
                  <td className="px-6 py-4 text-sm">{transfer.orderDate}</td>
                  <td className="px-6 py-4 text-sm">{transfer.state}</td>
                </motion.tr>
                {expandedRow === index && (
                  <tr>
                    <td colSpan="6" className="px-6 py-4">
                      <AnimatePresence>
                        <motion.div
                          initial={{ opacity: 0 }}
                          animate={{ opacity: 1 }}
                          exit={{ opacity: 0 }}
                          className="bg-gray-800 p-4 rounded-lg"
                        >
                          <h3 className="text-lg font-bold mb-2">Details</h3>
                          <table className="min-w-full border-spacing-y-4">
                            <thead>
                              <tr className="text-gray-100">
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Product Item ID</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Location Name</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Warehouse</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Entry Date</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Supplier</th>
                              </tr>
                            </thead>
                            <tbody>
                              {transfer.details.map((detail, detailIndex) => (
                                <tr key={detailIndex} className="text-gray-100">
                                  <td className="px-6 py-4 text-sm">{detail.productItemId}</td>
                                  <td className="px-6 py-4 text-sm">{detail.locationName}</td>
                                  <td className="px-6 py-4 text-sm">{detail.wareHouse}</td>
                                  <td className="px-6 py-4 text-sm">{detail.entryDate}</td>
                                  <td className="px-6 py-4 text-sm">{detail.supplier}</td>
                                </tr>
                              ))}
                            </tbody>
                          </table>
                        </motion.div>
                      </AnimatePresence>
                    </td>
                  </tr>
                )}
              </React.Fragment>
              : null
            ))}
          </tbody>
        </table>
      </div>
    </motion.div>
  );
};

export default TransferTable;