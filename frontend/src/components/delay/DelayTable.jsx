import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search, Clock } from 'lucide-react';
import dayjs from 'dayjs';

const foodProducts = [
  {
    name: "Milk",
    quantity: 5,
    manufacturer: "DairyCo",
    category: "Food",
    details: [
      { productItemId: 1, locationName: "Aisle1", wareHouse: "Main Warehouse", expirationDate: "2025-01-30", supplier: "Supplier A" },
      { productItemId: 2, locationName: "Aisle1", wareHouse: "Main Warehouse", expirationDate: "2025-02-05", supplier: "Supplier A" },
      { productItemId: 3, locationName: "Aisle1", wareHouse: "Main Warehouse", expirationDate: "2025-02-05", supplier: "Supplier A" }
    ]
  },
  {
    name: "Bread",
    quantity: 10,
    manufacturer: "BakeryCo",
    category: "Food",
    details: [
      { productItemId: 4, locationName: "Aisle2", wareHouse: "Main Warehouse", expirationDate: "2025-02-05", supplier: "Supplier B" },
      { productItemId: 5, locationName: "Aisle2", wareHouse: "Main Warehouse", expirationDate: "2025-01-30", supplier: "Supplier B" }
    ]
  }
];

const DelayTable = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredProducts, setFilteredProducts] = useState([]);
  const [expandedRow, setExpandedRow] = useState(null);

  useEffect(() => {
    const filtered = foodProducts.map((product) => {
      const riskedItems = product.details.filter((detail) => {
        const daysLeft = dayjs(detail.expirationDate).diff(dayjs(), 'day');
        return daysLeft <= 14;
      });
      return { ...product, details: riskedItems, quantity: riskedItems.length };
    }).filter((product) =>
      product.name.toLowerCase().includes(searchTerm) ||
      product.manufacturer.toLowerCase().includes(searchTerm) ||
      product.category.toLowerCase().includes(searchTerm)
    );
    setFilteredProducts(filtered);
  }, [searchTerm]);

  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
  };

  const handleRowClick = (index) => {
    setExpandedRow(expandedRow === index ? null : index);
  };

  const getBackgroundColor = (daysLeft) => {
    if (daysLeft <= 7) {
      return "bg-red-500 text-black";
    } else if (daysLeft <= 14) {
      return "bg-yellow-400 text-black";
    } else {
      return "";
    }
  };

  return (
    <motion.div
      className="ml-14 mt-2 w-11/12 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
    >
      <div className="relative flex justify-between items-center mb-4">
        <Clock size={50} style={{ color: "red", minWidth: "50px" }} className="absolute top-0 left-0" />
        <h2 className="text-2xl font-semibold text-gray-100 mx-auto">Products Near Expiration</h2>
        <div className="relative">
          <input
            type="text"
            placeholder="Search..."
            value={searchTerm}
            onChange={handleSearch}
            className="pl-10 p-2 rounded-xl bg-gray-800 text-gray-100 w-full max-w-xs"
          />
          <Search className='absolute right-[220px] top-2.5 text-gray-400 ' size={18} />
        </div>
      </div>
      <div className="overflow-x-auto">
        <table className="min-w-full border-spacing-y-4">
          <thead>
            <tr className="text-gray-100">
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Name</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Manufacturer</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
            </tr>
          </thead>
          <tbody>
            {filteredProducts.map((product, index) => (
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
                    <span className="font-bold text-lg">{product.name}</span>&nbsp;&nbsp;<span className='text-sm'>click for details</span>
                  </td>
                  <td className="px-6 py-4 text-sm">{product.quantity}</td>
                  <td className="px-6 py-4 text-sm">{product.manufacturer}</td>
                  <td className="px-6 py-4 text-sm">{product.category}</td>
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
                          <table className="min-w-full border-spacing-y-4">
                            <thead>
                              <tr className="text-gray-100">
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Product Item ID</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Location Name</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Warehouse</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Expiration Date</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Days Left</th>
                                <th className="px-6 py-3 text-left text-sm font-medium uppercase">Supplier</th>
                              </tr>
                            </thead>
                            <tbody>
                              {product.details.map((detail, detailIndex) => {
                                const daysLeft = dayjs(detail.expirationDate).diff(dayjs(), 'day');
                                return (
                                  <tr key={detailIndex} className="text-gray-100">
                                    <td className="px-6 py-4 text-sm">{detail.productItemId}</td>
                                    <td className="px-6 py-4 text-sm">{detail.locationName}</td>
                                    <td className="px-6 py-4 text-sm">{detail.wareHouse}</td>
                                    <td className="px-6 py-4 text-sm">{detail.expirationDate}</td>
                                    <td className="px-6 py-4 text-sm flex justify-center"><span className={`${getBackgroundColor(daysLeft)} rounded-lg px-4 py-2`}>{daysLeft} days</span></td>
                                    <td className="px-6 py-4 text-sm">{detail.supplier}</td>
                                  </tr>
                                );
                              })}
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
      </div>
    </motion.div>
  );
};

export default DelayTable;