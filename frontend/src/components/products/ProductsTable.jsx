import { motion } from "framer-motion";
import { Search, ShoppingBag } from "lucide-react";
import { useState, useEffect, useRef } from "react";
import { useProducts } from "../../context/ProductProvider";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const capitalize = (str) => {
  return str.charAt(0).toUpperCase() + str.slice(1);
};

const ProductsTable = ({ selectedList }) => {
  const { products } = useProducts();
  console.log("salem", products);
  const [selectedProducts, setSelectedProducts] = useState(products);
  const [searchTerm, setSearchTerm] = useState("");
  const [filteredProducts, setFilteredProducts] = useState(products);
  const notifiedProducts = useRef(new Set());

  useEffect(() => {
    setFilteredProducts(products);
  }, [products]);

  useEffect(() => {
    let filteredProducts;
    if (selectedList !== "all") {
      filteredProducts = products.filter((product) => product.category.toLowerCase() === selectedList.toLowerCase());
    } else {
      filteredProducts = products;
    }
    const filtered = filteredProducts.filter(
      (product) => product.name.toLowerCase().includes(searchTerm) ||
        product.category.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredProducts(filtered);
  }, [selectedList, searchTerm, products]);

  const handleSearch = (e) => {
    const term = e.target.value.toLowerCase();
    setSearchTerm(term);
  };

  console.log('filteredProducts', filteredProducts);

  if (!filteredProducts) {
    return <div>Loading...</div>;
  }

  return (
    <motion.div
      className="mt-4 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mb-8"
      initial={{ opacity: 0, y: 0 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
    >
      <div className="flex justify-between items-center mb-6">
        <ShoppingBag size={50} style={{ color: "#8B5CF6", minWidth: "50px" }} />
        <h2 className="text-4xl font-semibold text-gray-100">{selectedList.toLowerCase() === "all" ? 'All Products' : capitalize(selectedList) + ' Products'}</h2>
        <div className="relative">
          <input
            type="text"
            placeholder="Search products..."
            className="bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
            onChange={handleSearch}
            value={searchTerm}
          />
          <Search className="absolute left-3 top-2.5 text-gray-400" size={18} />
        </div>
      </div>

      <div className="overflow-x-auto">
        <table className="min-w-full border-spacing-y-4">
          <thead>
            <tr className="text-gray-100">
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Name</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Price</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Stock</th>
            </tr>
          </thead>
          <tbody>
            {filteredProducts.map((product) => (
              <motion.tr
                key={product.id}
                initial={{ opacity: 0 }}
                animate={{ opacity: 1 }}
                transition={{ duration: 0.3 }}
                className="text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700"
              >
                <td className="flex items-center px-6 py-4 text-sm">
                  {product.name}
                </td>
                <td className="px-6 py-4 text-sm">{capitalize(product.category)}</td>
                <td className="px-6 py-4 text-sm">${product.price.toFixed(2)}</td>
                <td className="px-6 py-4 text-sm">{product.stock}</td>
              </motion.tr>
            ))}
          </tbody>
        </table>
      </div>
    </motion.div>
  );
};

export default ProductsTable;