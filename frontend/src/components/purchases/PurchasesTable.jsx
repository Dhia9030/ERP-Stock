import React, { useState, useEffect } from 'react';
import { motion } from "framer-motion";
import { Search, Eye, ShoppingCart } from "lucide-react";
import { useNavigate } from "react-router-dom";
import * as SignalR from '@microsoft/signalr';
import { usePurchase } from "../../context/PurchaseProvider";

const PurchasesTable = () => {
  const { purchaseData, setPurchaseData } = usePurchase();
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredPurchases, setFilteredPurchases] = useState(purchaseData);
  const navigate = useNavigate();

  console.log('PurchasesTable purchaseData:', purchaseData);

  useEffect(() => {
    setFilteredPurchases(purchaseData);
  }, [purchaseData]);

  useEffect(() => {
    // Create a connection to the SignalR hub
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/purchasehub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub");

        // Listen for updates from the server
        connection.on("ReceivePurchases", (data) => {
          console.log("Received purchases:", data);
          setPurchaseData(data);
        });

        // Optionally, you can invoke a method on the server to request initial purchases
        connection.invoke("GetInitialPurchases")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    // Clean up the connection when the component unmounts
    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
  }, [setPurchaseData]);

  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
    const filtered = purchaseData.filter(
      (purchase) => purchase.id.toLowerCase().includes(term) || purchase.supplier.toLowerCase().includes(term)
    );
    setFilteredPurchases(filtered);
  };

  const handleViewPurchase = (purchaseId) => {
    navigate(`/purchases/${purchaseId}`);
  };

  return (
    <motion.div
      className='bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border '
      initial={{ opacity: 0, y: 0 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
    >
      <div className='flex justify-between items-center mb-6'>
        <ShoppingCart size={50} style={{ color: "#F59E0B", minWidth: "50px" }} />
        <h2 className='text-4xl font-semibold text-gray-100'>Purchase List</h2>
        <div className='relative'>
          <input
            type='text'
            placeholder='Search purchases...'
            className='bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500'
            value={searchTerm}
            onChange={handleSearch}
          />
          <Search className='absolute left-3 top-2.5 text-gray-400' size={18} />
        </div>
      </div>

      <div className='overflow-x-auto'>
        <table className='min-w-full divide-y divide-gray-700'>
          <thead>
            <tr>
              <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                Purchase ID
              </th>
              <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                Supplier
              </th>
              <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                Total
              </th>
              <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                Status
              </th>
              <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                Date
              </th>
              <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                Actions
              </th>
            </tr>
          </thead>

          <tbody className='divide divide-gray-700'>
            {filteredPurchases.map((purchase) => (
              <motion.tr
                key={purchase.id}
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ duration: 0.3 }}
                onClick={() => handleViewPurchase(purchase.id)}
                className='cursor-pointer border-b-[1px] border-gray-700 hover:bg-gray-800  '
              >
                <td className='px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-100'>
                  {purchase.id}
                </td>
                <td className='px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-100'>
                  {purchase.supplier}
                </td>
                <td className='px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-100'>
                  ${purchase.total.toFixed(2)}
                </td>
                <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                  <span
                    className={`px-2 inline-flex text-xs leading-5 font-semibold rounded-full ${
                      purchase.executed ? "bg-green-100 text-green-800" : "bg-yellow-100 text-yellow-800"
                    }`}
                  >
                    {purchase.executed ? "Executed" : "Not Executed"}
                  </span>
                </td>
                <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>{purchase.orderDate}</td>
                <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                  <button className='text-indigo-400 hover:text-indigo-300 mr-2'>
                    <Eye size={18} />
                  </button>
                </td>
              </motion.tr>
            ))}
          </tbody>
        </table>
      </div>
    </motion.div>
  );
};

export default PurchasesTable;