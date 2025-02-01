import React, { useState, useEffect } from 'react';
import { AlertTriangle, Search } from 'lucide-react';
import * as SignalR from '@microsoft/signalr';
import { useProducts } from '../../context/ProductProvider';

const LowStockTable = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [lowproducts, setLowStockProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState(lowproducts);

  const ClothingMin = 300;
  const ElectronicsMin = 300;
  const FoodMin = 300;

  const PRODUCT_DATA = useProducts();
  useEffect(() => {

    if (PRODUCT_DATA && PRODUCT_DATA.length > 0) {
      const lowStockProducts = PRODUCT_DATA.filter(product => {
        if (product.category === "Clothing") {
          return product.stock < ClothingMin;
        } else if (product.category === "Electronics") {
          return product.stock < ElectronicsMin;
        } else if (product.category === "Food") {
          return product.stock < FoodMin;
        }
        return false;
      });


      setLowStockProducts(lowStockProducts);
      setFilteredProducts(lowStockProducts);
    }
  }, [PRODUCT_DATA]);


  useEffect(() => {
    const filtered = lowproducts.filter((product) =>
      product.name.toLowerCase().includes(searchTerm) ||
      product.manufacturer.toLowerCase().includes(searchTerm) ||
      product.category.toLowerCase().includes(searchTerm)
    );
    setFilteredProducts(filtered);
  }, [searchTerm, lowproducts]); // Include lowproducts in the dependency array

  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
  };

  return (
    <div className="ml-14 mt-2 w-11/12 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8">
      <div className="relative mb-10">
        <AlertTriangle size={50} style={{ color: "#F59E0B", minWidth: "50px" }} className="absolute top-0 left-0" />
        <h2 className='text-4xl font-semibold text-gray-100 text-center'>Low Stock</h2>
        <div className="absolute top-0 right-0 flex items-center">
          <input
            type="text"
            placeholder="Search..."
            value={searchTerm}
            onChange={handleSearch}
            className="pl-10 p-2 rounded-xl bg-gray-800 text-gray-100"
          />
          <Search className='absolute right-3 top-2.5 text-gray-400' size={18} />
        </div>
      </div>
      <div className="overflow-x-auto">
        <table className="min-w-full border-spacing-y-4">
          <thead>
            <tr className="text-gray-100">
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Name</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Quantity</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
            </tr>
          </thead>
          <tbody>
            {filteredProducts.map((product, index) => (
              <tr key={index} className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700">
                <td className="flex items-center px-6 py-4 text-sm">
                  {product.name}
                </td>
                <td className="px-6 py-4 text-sm">{product.stock}</td>
                <td className="px-6 py-4 text-sm">{product.category}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default LowStockTable;