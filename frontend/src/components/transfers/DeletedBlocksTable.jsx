import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import * as SignalR from '@microsoft/signalr';
import {useTransfer} from '../../context/TransferProvider';

const mergedBlocks = [
  {
    sourceBlockId: 1,
    sourceQuantity: 5,
    destinationBlockId: 2,
    destinationQuantity: 10,
    location: "Aisle1",
    warehouse: "Main Warehouse",
    newQuantity: 15
  },
  {
    sourceBlockId: 3,
    sourceQuantity: 8,
    destinationBlockId: 4,
    destinationQuantity: 12,
    location: "Aisle2",
    warehouse: "Secondary Warehouse",
    newQuantity: 20
  }
];

const MergedBlocksTable = () => {
  const [blocks, setBlocks] = useState(mergedBlocks);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredBlocks, setFilteredBlocks] = useState(mergedBlocks);
  const [expandedRow, setExpandedRow] = useState(null);


  const {transfers} = useTransfer();
  //console.log("transfer wa : ", transfers)

  useEffect(() => {
    // Uncomment the following code to fetch data from SignalR
    /*
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/transferhub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub for merged blocks");

        connection.on("ReceiveMergedBlocks", (data) => {
          console.log("Received merged blocks:", data);
          setBlocks(data);
          setFilteredBlocks(data);
        });

        connection.invoke("GetInitialMergedBlocks")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
    */
  }, []);

  useEffect(() => {
    const filtered = blocks.filter((block) =>
      block.location.toLowerCase().includes(searchTerm) ||
      block.warehouse.toLowerCase().includes(searchTerm)
    );
    setFilteredBlocks(filtered);
  }, [searchTerm, blocks]);

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
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Source Block ID</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Source Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Destination Block ID</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Destination Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Warehouse</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">New Quantity</th>
            </tr>
          </thead>
          <tbody>
            {filteredBlocks.map((block, index) => (
              <React.Fragment key={index}>
                <motion.tr
                  onClick={() => handleRowClick(index)}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ duration: 0.3 }}
                  className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
                >
                  <td className="px-6 py-4 text-sm">{block.sourceBlockId}</td>
                  <td className="px-6 py-4 text-sm">{block.sourceQuantity}</td>
                  <td className="px-6 py-4 text-sm">{block.destinationBlockId}</td>
                  <td className="px-6 py-4 text-sm">{block.destinationQuantity}</td>
                  <td className="px-6 py-4 text-sm">{block.location}</td>
                  <td className="px-6 py-4 text-sm">{block.warehouse}</td>
                  <td className="px-6 py-4 text-sm">{block.newQuantity}</td>
                </motion.tr>
                {expandedRow === index && (
                  <tr>
                    <td colSpan="7" className="px-6 py-4">
                      <AnimatePresence>
                        <motion.div
                          initial={{ opacity: 0 }}
                          animate={{ opacity: 1 }}
                          exit={{ opacity: 0 }}
                          className="bg-gray-800 p-4 rounded-lg"
                        >
                          <h3 className="text-lg font-bold mb-2">Details</h3>
                          <p className="text-gray-100">Source Block ID: {block.sourceBlockId}</p>
                          <p className="text-gray-100">Source Quantity: {block.sourceQuantity}</p>
                          <p className="text-gray-100">Destination Block ID: {block.destinationBlockId}</p>
                          <p className="text-gray-100">Destination Quantity: {block.destinationQuantity}</p>
                          <p className="text-gray-100">Location: {block.location}</p>
                          <p className="text-gray-100">Warehouse: {block.warehouse}</p>
                          <p className="text-gray-100">New Quantity: {block.newQuantity}</p>
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

export default MergedBlocksTable;