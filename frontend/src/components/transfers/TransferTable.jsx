import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import { useTransfer } from '../../context/TransferProvider';

const TransferTable = ({ type }) => {
  const { useImport, useExport } = useTransfer();
  const importTransfers = useImport();
  const exportTransfers = useExport();
  const [transfers, setTransfers] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredTransfers, setFilteredTransfers] = useState([]);
  const [expandedRow, setExpandedRow] = useState(null);
  const [showAll, setShowAll] = useState(false);

  useEffect(() => {
    if (type === "IN") {
      setTransfers(importTransfers);
    } else {
      setTransfers(exportTransfers);
    }
  }, [type, importTransfers, exportTransfers]);

  useEffect(() => {
    const filtered = transfers.filter((transfer) =>
      (transfer.productName?.toLowerCase().includes(searchTerm)) ||
      (transfer.categoryName?.toLowerCase().includes(searchTerm)) ||
      (transfer.sourceLocation?.toLowerCase().includes(searchTerm)) ||
      (transfer.destinationLocation?.toLowerCase().includes(searchTerm)) ||
      (transfer.date?.toLowerCase().includes(searchTerm))
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

  const handleShowAllClick = () => {
    setShowAll(!showAll);
  };

  const displayedTransfers = showAll ? filteredTransfers : filteredTransfers.slice(0, 7);

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
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Product Name</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Source Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Destination Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Date</th>
            </tr>
          </thead>
          <tbody>
            {displayedTransfers.map((transfer, index) => (
              <React.Fragment key={index}>
                <motion.tr
                  onClick={() => handleRowClick(index)}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ duration: 0.3 }}
                  className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
                >
                  <td className="flex items-center px-6 py-4 text-sm">
                    <span className='font-bold text-lg'>{transfer.productName}</span><span className='ml-3 text-xs'>click for details</span>
                  </td>
                  <td className="px-6 py-4 text-sm">{transfer.quantity}</td>
                  <td className="px-6 py-4 text-sm">{transfer.categoryName}</td>
                  <td className="px-6 py-4 text-sm">{transfer.sourceLocation}</td>
                  <td className="px-6 py-4 text-sm">{transfer.destinationLocation}</td>
                  <td className="px-6 py-4 text-sm">{transfer.date}</td>
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
                              </tr>
                            </thead>
                            <tbody>
                              {transfer.productItemIds.map((itemId, detailIndex) => (
                                <tr key={detailIndex} className="text-gray-100">
                                  <td className="px-6 py-4 text-sm">{itemId}</td>
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
            ))}
          </tbody>
        </table>
        {filteredTransfers.length > 7 && (
          <div className="text-center mt-4">
            <button
              onClick={handleShowAllClick}
              className="px-4 py-2 bg-blue-400 text-white rounded-lg"
            >
              {showAll ? 'Show Less' : 'See All'}
            </button>
          </div>
        )}
      </div>
    </motion.div>
  );
};

export default TransferTable;