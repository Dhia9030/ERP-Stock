import React, { useState, useEffect, useCallback } from 'react';
import { motion } from "framer-motion";
import { Search, ShoppingCart } from "lucide-react";
import { useNavigate } from "react-router-dom";
import { debounce } from 'lodash';
import { usePurchase } from "../../context/PurchaseProvider";

const PurchasesTable = () => {
  const { purchaseData } = usePurchase();
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredPurchases, setFilteredPurchases] = useState(purchaseData);
  const navigate = useNavigate();

  useEffect(() => {
    setFilteredPurchases(purchaseData);
  }, [purchaseData]);

  const handleSearch = useCallback(
    debounce((term) => {
      const filtered = purchaseData.filter(
        (purchase) =>
          purchase.id.toLowerCase().includes(term) ||
          purchase.supplier.toLowerCase().includes(term)
      );
      setFilteredPurchases(filtered);
    }, 300),
    [purchaseData]
  );

  const onSearchChange = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
    handleSearch(term);
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
            onChange={onSearchChange}
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
                      purchase.status === 1
                        ? "bg-green-100 text-green-800"
                        : purchase.status === 0
                        ? "bg-yellow-100 text-yellow-800"
                        : "bg-blue-100 text-blue-800"
                    }`}
                  >
                    {purchase.status === 1
                      ? "Executed"
                      : purchase.status === 0
                      ? "Not Executed"
                      : "Received"}
                  </span>
                </td>
                <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                  {purchase.orderDate}
                </td>
                <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'></td>
              </motion.tr>
            ))}
          </tbody>
        </table>
      </div>
    </motion.div>
  );
};

export default PurchasesTable;