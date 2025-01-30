import React, { useState, useEffect } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import { Search } from 'lucide-react';
import * as SignalR from '@microsoft/signalr';

const foodProducts = [
  {
    name: "Milk",
    productBlockId: 1,
    location: "Aisle1",
    warehouse: "Main Warehouse",
    expirationDate: "2025-01-30",
    supplier: "Supplier A"
  },
  {
    name: "Bread",
    productBlockId: 2,
    location: "Aisle2",
    warehouse: "Main Warehouse",
    expirationDate: "2025-02-05",
    supplier: "Supplier B"
  }
];

const DelayTable = () => {
  const [products, setProducts] = useState(foodProducts);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredProducts, setFilteredProducts] = useState(foodProducts);
  const [expandedRow, setExpandedRow] = useState(null);

  useEffect(() => {
    // Uncomment the following code to fetch data from SignalR
    /*
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/delayhub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub for delay products");

        connection.on("ReceiveDelayProducts", (data) => {
          console.log("Received delay products:", data);
          setProducts(data);
          setFilteredProducts(data);
        });

        connection.invoke("GetInitialDelayProducts")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
    */
  }, []);

  useEffect(() => {
    const filtered = products.filter((product) =>
      product.name.toLowerCase().includes(searchTerm) ||
      product.location.toLowerCase().includes(searchTerm) ||
      product.warehouse.toLowerCase().includes(searchTerm)
    );
    setFilteredProducts(filtered);
  }, [searchTerm, products]);

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
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Expiration Date</th>
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
                  <td className="px-6 py-4 text-sm">{product.name}</td>
                  <td className="px-6 py-4 text-sm">{product.productBlockId}</td>
                  <td className="px-6 py-4 text-sm">{product.location}</td>
                  <td className="px-6 py-4 text-sm">{product.warehouse}</td>
                  <td className="px-6 py-4 text-sm">{product.expirationDate}</td>
                </motion.tr>
                {expandedRow === index && (
                  <tr>
                    <td colSpan="5" className="px-6 py-4">
                      <AnimatePresence>
                        <motion.div
                          initial={{ opacity: 0 }}
                          animate={{ opacity: 1 }}
                          exit={{ opacity: 0 }}
                          className="bg-gray-800 p-4 rounded-lg"
                        >
                          <h3 className="text-lg font-bold mb-2">Details</h3>
                          <p className="text-gray-100">Product: {product.name}</p>
                          <p className="text-gray-100">Product Block ID: {product.productBlockId}</p>
                          <p className="text-gray-100">Location: {product.location}</p>
                          <p className="text-gray-100">Warehouse: {product.warehouse}</p>
                          <p className="text-gray-100">Expiration Date: {product.expirationDate}</p>
                          <p className="text-gray-100">Supplier: {product.supplier}</p>
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