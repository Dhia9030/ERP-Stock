import React, { useState, useEffect } from 'react';
import { motion } from 'framer-motion';

const products = [
  {
    name: "Phone",
    productId: 1,
    blocks: [
      { productBlockId: 1, location: "Aisle1", warehouse: "Main Warehouse", quantity: 10 },
      { productBlockId: 2, location: "Aisle2", warehouse: "Main Warehouse", quantity: 5 },
    ]
  },
  {
    name: "Laptop",
    productId: 2,
    blocks: [
      { productBlockId: 3, location: "Aisle3", warehouse: "Main Warehouse", quantity: 8 },
      { productBlockId: 4, location: "Aisle4", warehouse: "Main Warehouse", quantity: 12 },
    ]
  }
];

const freeLocations = [
  { locationId: 1, name: "Aisle5", warehouse: "Main Warehouse" },
  { locationId: 2, name: "Aisle6", warehouse: "Main Warehouse" },
  { locationId: 3, name: "Aisle7", warehouse: "Secondary Warehouse" },
  { locationId: 4, name: "Aisle8", warehouse: "Secondary Warehouse" }
];

const InternalTransfer = () => {
  const [operation, setOperation] = useState("transfer");
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [sourceBlock, setSourceBlock] = useState(null);
  const [destinationLocation, setDestinationLocation] = useState(null);
  const [productBlocks, setProductBlocks] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    if (selectedProduct) {
      const product = products.find(p => p.productId === parseInt(selectedProduct));
      setProductBlocks(product ? product.blocks : []);
    }
  }, [selectedProduct]);

  const handleSearch = (e) => {
    setSearchTerm(e.target.value.toLowerCase());
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    const requestData = {
      operation,
      productId: selectedProduct,
      sourceBlockId: sourceBlock,
      destinationLocation
    };
    console.log("Request Data:", requestData);
    // Send request to API endpoint
    // axios.post('/api/transfer', requestData)
    //   .then(response => console.log(response))
    //   .catch(error => console.error(error));
  };

  return (
    <motion.div
      className="ml-14 mt-2 w-11/12 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
    >
      <div className="relative">
        <h1 className='text-center text-2xl font-bold'>Create an Internal Transfer</h1>
      </div>
      <form onSubmit={handleSubmit}>
        <div className="mb-4">
          <label className="block text-white text-sm font-bold mb-2">Operation</label>
          <select
            value={operation}
            onChange={(e) => setOperation(e.target.value)}
            className="w-full p-2 rounded-xl bg-gray-800 text-white"
          >
            <option value="transfer">Transfer</option>
            <option value="merge">Merge</option>
            <option value="delete">Delete</option>
          </select>
        </div>
        <div className="mb-4">
          <label className="block text-white text-sm font-bold mb-2">Product</label>
          <select
            value={selectedProduct}
            onChange={(e) => setSelectedProduct(e.target.value)}
            className="w-full p-2 rounded-xl bg-gray-800 text-white"
          >
            <option value="">Select Product</option>
            {products.map(product => (
              <option key={product.productId} value={product.productId}>{product.name}</option>
            ))}
          </select>
        </div>
        {operation === "transfer" && (
          <>
            <div className="mb-4">
              <label className="block text-white text-sm font-bold mb-2">Product Block</label>
              <select
                value={sourceBlock}
                onChange={(e) => setSourceBlock(e.target.value)}
                className="w-full p-2 rounded-xl bg-gray-800 text-white"
              >
                <option value="">Select Product Block</option>
                {productBlocks.map(block => (
                  <option key={block.productBlockId} value={block.productBlockId}>
                    {block.productBlockId} - {block.location} - {block.warehouse}
                  </option>
                ))}
              </select>
            </div>
            <div className="mb-4">
              <label className="block text-white text-sm font-bold mb-2">Destination Location</label>
              <select
                value={destinationLocation}
                onChange={(e) => setDestinationLocation(e.target.value)}
                className="w-full p-2 rounded-xl bg-gray-800 text-white"
              >
                <option value="">Select Destination Location</option>
                {freeLocations.map(location => (
                  <option key={location.locationId} value={`${location.name} - ${location.warehouse}`}>
                    {location.name} - {location.warehouse}
                  </option>
                ))}
              </select>
            </div>
          </>
        )}
        {operation === "merge" && (
          <>
            <div className="mb-4">
              <label className="block text-white text-sm font-bold mb-2">Source Product Block</label>
              <select
                value={sourceBlock}
                onChange={(e) => setSourceBlock(e.target.value)}
                className="w-full p-2 rounded-xl bg-gray-800 text-white"
              >
                <option value="">Select Source Product Block</option>
                {productBlocks.map(block => (
                  <option key={block.productBlockId} value={block.productBlockId}>
                    {block.productBlockId} - {block.location} - {block.warehouse} - Quantity: {block.quantity}
                  </option>
                ))}
              </select>
            </div>
            <div className="mb-4">
              <label className="block text-white text-sm font-bold mb-2">Destination Product Block</label>
              <select
                value={destinationLocation}
                onChange={(e) => setDestinationLocation(e.target.value)}
                className="w-full p-2 rounded-xl bg-gray-800 text-white"
              >
                <option value="">Select Destination Product Block</option>
                {productBlocks.filter(block => block.productBlockId !== parseInt(sourceBlock)).map(block => (
                  <option key={block.productBlockId} value={block.productBlockId}>
                    {block.productBlockId} - {block.location} - {block.warehouse} - Quantity: {block.quantity}
                  </option>
                ))}
              </select>
            </div>
          </>
        )}
        {operation === "delete" && (
          <div className="mb-4">
            <label className="block text-white text-sm font-bold mb-2">Product Block</label>
            <select
              value={sourceBlock}
              onChange={(e) => setSourceBlock(e.target.value)}
              className="w-full p-2 rounded-xl bg-gray-800 text-white"
            >
              <option value="">Select Product Block</option>
              {productBlocks.map(block => (
                <option key={block.productBlockId} value={block.productBlockId}>
                  {block.productBlockId} - {block.location} - {block.warehouse}
                </option>
              ))}
            </select>
          </div>
        )}
        <button
          type="submit"
          className="w-full p-2 rounded-xl bg-green-500 text-white font-semibold hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-green-400 focus:ring-opacity-75"
        >
          Execute
        </button>
      </form>
    </motion.div>
  );
};

export default InternalTransfer;