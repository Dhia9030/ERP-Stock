import React, { useState, useEffect, useMemo, useCallback } from 'react';
import { motion } from 'framer-motion';
import * as SignalR from '@microsoft/signalr';
import { toast } from 'react-toastify';
import debounce from 'lodash.debounce';

const InternalTransfer = () => {
  const [operation, setOperation] = useState("transfer");
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [sourceBlock, setSourceBlock] = useState(null);
  const [destinationLocation, setDestinationLocation] = useState(null);
  const [products, setProducts] = useState([]);
  const [productBlocks, setProductBlocks] = useState([]);
  const [freeLocations, setFreeLocations] = useState([]);
  const [searchTerm, setSearchTerm] = useState("");
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchLocation = async () => {
      try {
        setLoading(true);
        const response = await fetch('http://localhost:5188/Test/get free location');
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setFreeLocations(data);
      } catch (error) {
        console.error('Failed to fetch free locations:', error);
        toast.error('Failed to fetch free locations');
      } finally {
        setLoading(false);
      }
    };

    fetchLocation();
  }, []);

  const fetchProducts = useCallback(async () => {
    try {
      setLoading(true);
      const response = await fetch('http://localhost:5188/Test/get all product with blocks');
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      const data = await response.json();
      setProducts(data);
    } catch (error) {
      console.error('Failed to fetch products:', error);
      toast.error('Failed to fetch products');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    fetchProducts();
  }, [fetchProducts]);

  useEffect(() => {
    if (selectedProduct) {
      const product = products.find(p => p.ProductName === selectedProduct);
      setProductBlocks(product ? product.ProductBlocks : []);
    }
  }, [selectedProduct, products]);

  const debouncedSearch = useMemo(() => debounce((value) => {
    setSearchTerm(value.toLowerCase());
  }, 300), []);

  const handleSearch = (e) => {
    debouncedSearch(e.target.value);
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    let requestData = {};
    let endpoint = '';

    if (operation === "transfer") {
        requestData = {
            productBlockId: parseInt(sourceBlock, 10),
            newLocationId: parseInt(destinationLocation, 10)
        };
        endpoint = 'http://localhost:5188/Test/transferproductblock';
    } else if (operation === "merge") {
        requestData = {
            sourceBlockId: parseInt(sourceBlock, 10),
            destinationBlockId: parseInt(destinationLocation, 10)
        };
        endpoint = 'http://localhost:5188/Test/mergeproductblocks';
    } else if (operation === "delete") {
        requestData = {
            productBlockId: parseInt(sourceBlock, 10)
        };
        endpoint = 'http://localhost:5188/Test/delete product block';
    }

    try {
        const response = await fetch(endpoint, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Accept': '*/*'
            },
            body: JSON.stringify(requestData)
        });

        if (!response.ok) {
            const errorText = await response.text();
            throw new Error(`Request failed with status ${response.status}: ${errorText}`);
        }

        const data = await response.json();
        console.log('Success:', data);
        toast.success('Operation completed successfully');
    } catch (error) {
        console.error('Error:', error);
        toast.error('Failed to execute operation');
    }
};

  const filteredProducts = useMemo(() => {
    return (operation === "merge"
      ? products.filter(product => product.CategoryName !== "Food")
      : products).filter(product =>
        product.ProductName.toLowerCase().includes(searchTerm)
      );
  }, [products, searchTerm, operation]);

  const selectedProductCategory = products.find(p => p.ProductName === selectedProduct)?.CategoryName;

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
            className="custom-select w-full p-2 rounded-xl bg-gray-800 text-white"
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
            {filteredProducts.map(product => (
              <option key={product.ProductName} value={product.ProductName}>{product.ProductName}</option>
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
                  <option key={block.ProductBlockId} value={block.ProductBlockId}>
                    {block.ProductBlockId} - {block.LocationName}
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
                  <option key={location.LocationId} value={location.LocationId}>
                    {location.Name}
                  </option>
                ))}
              </select>
            </div>
          </>
        )}
        {operation === "merge" && selectedProductCategory !== "Food" && (
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
                  <option key={block.ProductBlockId} value={block.ProductBlockId}>
                    {block.ProductBlockId} - {block.LocationName} - Quantity: {block.quantity}
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
                {productBlocks.filter(block => block.ProductBlockId !== parseInt(sourceBlock)).map(block => (
                  <option key={block.ProductBlockId} value={block.ProductBlockId}>
                    {block.ProductBlockId} - {block.LocationName} - Quantity: {block.quantity}
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
                <option key={block.ProductBlockId} value={block.ProductBlockId}>
                  {block.ProductBlockId} - {block.LocationName}
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