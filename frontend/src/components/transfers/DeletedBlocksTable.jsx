import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import { useTransfer } from '../../context/TransferProvider';

const DeletedBlocksTable = () => {
  const { useDeleted } = useTransfer();
  const deletedProducts = useDeleted();
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredBlocks, setFilteredBlocks] = useState(deletedProducts);
  const [expandedRow, setExpandedRow] = useState(null);
  const [showAll, setShowAll] = useState(false);

  console.log('Deleted Blocks mn 3nd si sahbi:', deletedProducts);

  useEffect(() => {
    setFilteredBlocks(deletedProducts);
  }, [deletedProducts]);

  useEffect(() => {
    const filtered = deletedProducts.filter((block) =>
      (block.sourceLocation && block.sourceLocation.toLowerCase().includes(searchTerm)) ||
      (block.destinationLocation && block.destinationLocation.toLowerCase().includes(searchTerm))
    );
    setFilteredBlocks(filtered);
  }, [searchTerm, deletedProducts]);

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

  const displayedBlocks = showAll ? filteredBlocks : filteredBlocks.slice(0, 7);

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
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Source Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Destination Location</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Date</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Created By</th>
            </tr>
          </thead>
          <tbody>
            {displayedBlocks.map((block, index) => (
              <React.Fragment key={index}>
                <motion.tr
                  onClick={() => handleRowClick(index)}
                  initial={{ opacity: 0 }}
                  animate={{ opacity: 1 }}
                  transition={{ duration: 0.3 }}
                  className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
                >
                  <td className="px-6 py-4 text-sm">{block.productName} <span className='font-extralight text-sm'>(click for details)</span></td>
                  <td className="px-6 py-4 text-sm">{block.categoryName}</td>
                  <td className="px-6 py-4 text-sm">{block.quantity}</td>
                  <td className="px-6 py-4 text-sm">{block.sourceLocation}</td>
                  <td className="px-6 py-4 text-sm">{block.destinationLocation}</td>
                  <td className="px-6 py-4 text-sm">{block.date}</td>
                  <td className="px-6 py-4 text-sm">{block.createdBy}</td>
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
                          <h3 className="text-lg font-bold mb-2">Product Item IDs</h3>
                          <ul className="list-disc list-inside text-gray-100">
                            {block.productItemIds.map((itemId, idx) => (
                              <li key={idx}>{itemId}</li>
                            ))}
                          </ul>
                        </motion.div>
                      </AnimatePresence>
                    </td>
                  </tr>
                )}
              </React.Fragment>
            ))}
          </tbody>
        </table>
        {filteredBlocks.length > 7 && (
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

export default DeletedBlocksTable;