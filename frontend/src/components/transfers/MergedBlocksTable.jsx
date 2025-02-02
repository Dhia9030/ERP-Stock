import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import { useTransfer } from '../../context/TransferProvider';

const MergedBlocksTable = () => {
  const { useMerge } = useTransfer();
  const mergedBlocks = useMerge();
  console.log('Merged Blocks mn 3nd si sahbi:', mergedBlocks);

  const [blocks, setBlocks] = useState(mergedBlocks);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredBlocks, setFilteredBlocks] = useState([]);
  const [expandedRow, setExpandedRow] = useState(null);

  useEffect(() => {
    setBlocks(mergedBlocks);
    setFilteredBlocks(mergedBlocks);
  }, [mergedBlocks]);

  useEffect(() => {
    const filtered = blocks.filter((block) => {
      if (!block || !block.sourceLocation || !block.destinationLocation || !block.productName) return false;
      return (
        block.sourceLocation.toLowerCase().includes(searchTerm) ||
        block.destinationLocation.toLowerCase().includes(searchTerm) ||
        block.productName.toLowerCase().includes(searchTerm)
      );
    });
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
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">ID</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Product Name</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Source Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Destination Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Date</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Created By</th>
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
                  <td className="px-6 py-4 text-sm">{block.id}</td>
                  <td className="px-6 py-4 text-sm">{block.productName}</td>
                  <td className="px-6 py-4 text-sm">{block.categoryName}</td>
                  <td className="px-6 py-4 text-sm">{block.quantity}</td>
                  <td className="px-6 py-4 text-sm">{block.sourceLocation}</td>
                  <td className="px-6 py-4 text-sm">{block.destinationLocation}</td>
                  <td className="px-6 py-4 text-sm">{block.date}</td>
                  <td className="px-6 py-4 text-sm">{block.createdBy}</td>
                </motion.tr>
                {expandedRow === index && (
                  <tr>
                    <td colSpan="8" className="px-6 py-4">
                      <AnimatePresence>
                        <motion.div
                          initial={{ opacity: 0 }}
                          animate={{ opacity: 1 }}
                          exit={{ opacity: 0 }}
                          className="bg-gray-800 p-4 rounded-lg"
                        >
                          <h3 className="text-lg font-bold mb-2">Details</h3>
                          <p className="text-gray-100">ID: {block.id}</p>
                          <p className="text-gray-100">Product Name: {block.productName}</p>
                          <p className="text-gray-100">Category: {block.categoryName}</p>
                          <p className="text-gray-100">Quantity: {block.quantity}</p>
                          <p className="text-gray-100">Source Location: {block.sourceLocation}</p>
                          <p className="text-gray-100">Destination Location: {block.destinationLocation}</p>
                          <p className="text-gray-100">Date: {block.date}</p>
                          <p className="text-gray-100">Created By: {block.createdBy}</p>
                          <p className="text-gray-100">Product Item IDs: {block.productItemIds.join(', ')}</p>
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