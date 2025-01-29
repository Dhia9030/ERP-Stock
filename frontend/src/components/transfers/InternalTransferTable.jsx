import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import * as SignalR from '@microsoft/signalr';

const internalTransfers = [
  {
    name: "Phone",
    productBlockId: 1,
    location: "Aisle1",
    warehouse: "Main Warehouse"
  },
  {
    name: "Laptop",
    productBlockId: 2,
    location: "Aisle2",
    warehouse: "Main Warehouse"
  }
];

const InternalTransferTable = () => {
  const [transfers, setTransfers] = useState(internalTransfers);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredTransfers, setFilteredTransfers] = useState(internalTransfers);
  const [expandedRow, setExpandedRow] = useState(null);

  useEffect(() => {
    // Uncomment the following code to fetch data from SignalR
    /*
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/transferhub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub for internal transfers");

        connection.on("ReceiveInternalTransfers", (data) => {
          console.log("Received internal transfers:", data);
          setTransfers(data);
          setFilteredTransfers(data);
        });

        connection.invoke("GetInitialInternalTransfers")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
    */
  }, []);

  useEffect(() => {
    const filtered = transfers.filter((transfer) =>
      transfer.name.toLowerCase().includes(searchTerm) ||
      transfer.location.toLowerCase().includes(searchTerm) ||
      transfer.warehouse.toLowerCase().includes(searchTerm)
    );
    setFilteredTransfers(filtered);
  }, [searchTerm, transfers]);

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
          className="pl-12 mb-4 p-2 rounded-xl bg-gray-800"
        />
        <Search className='absolute left-3 top-2.5 text-gray-400' size={18} />
      </div>
      
      <div className="overflow-x-auto">
        <table className="min-w-full border-spacing-y-4">
          <thead>
            <tr className="text-gray-100">
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Product</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Product Block ID</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Warehouse</th>
            </tr>
          </thead>
          <tbody>
            {filteredTransfers.map((transfer, index) => (
              <React.Fragment key={index}>
                <motion.tr
                  onClick={() => handleRowClick(index)}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ duration: 0.3 }}
                  className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
                >
                  <td className="px-6 py-4 text-sm">{transfer.name}</td>
                  <td className="px-6 py-4 text-sm">{transfer.productBlockId}</td>
                  <td className="px-6 py-4 text-sm">{transfer.location}</td>
                  <td className="px-6 py-4 text-sm">{transfer.warehouse}</td>
                </motion.tr>
                {expandedRow === index && (
                  <tr>
                    <td colSpan="4" className="px-6 py-4">
                      <AnimatePresence>
                        <motion.div
                          initial={{ opacity: 0 }}
                          animate={{ opacity: 1 }}
                          exit={{ opacity: 0 }}
                          className="bg-gray-800 p-4 rounded-lg"
                        >
                          <h3 className="text-lg font-bold mb-2">Details</h3>
                          <p className="text-gray-100">Product: {transfer.name}</p>
                          <p className="text-gray-100">Product Block ID: {transfer.productBlockId}</p>
                          <p className="text-gray-100">Location: {transfer.location}</p>
                          <p className="text-gray-100">Warehouse: {transfer.warehouse}</p>
                        </motion.div>
                      </AnimatePresence>
                    </td>
                  </tr>
                )}
              </React.Fragment>
            ))}
          </tbody>
        </table>
      </div>
    </motion.div>
  );
};

export default InternalTransferTable;