import { useState, useEffect } from "react";
import { motion } from "framer-motion";
import { Search, Package } from "lucide-react";

const ProductsInfo = () => {
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [expandedCategories, setExpandedCategories] = useState({});
  const [searchTerm, setSearchTerm] = useState("");
  const [statusFilter, setStatusFilter] = useState("available");

  useEffect(() => {
    fetch("http://localhost:5188/Test/get all product with blocks", {
      headers: { Accept: "*/*" },
    })
      .then((response) => response.json())
      .then((data) => {
        setProducts(data);
        setLoading(false);
      })
      .catch((error) => {
        console.error("Error fetching products:", error);
        setLoading(false);
      });
  }, []);

  const toggleCategory = (categoryName) => {
    setExpandedCategories((prev) => ({
      ...prev,
      [categoryName]: !prev[categoryName],
    }));
  };

  const getStatusText = (status) => {
    const statusMap = {
      0: "Available",
      1: "Out of Stock",
      2: "Expired",
      3: "Pending",
      5: "Merged By An Other Block (Not Available)",
    };
    return statusMap[status] || "Unknown Status";
  };

  const getStatusColor = (status) => {
    const colorMap = {
      0: "bg-green-500",
      1: "bg-red-500",
      2: "bg-yellow-500",
      3: "bg-orange-500",
      5: "bg-gray-500",
    };
    return colorMap[status] || "bg-gray-500";
  };

  const filteredProducts = products.filter((product) => {
    const matchesSearch =
      product.ProductName.toLowerCase().includes(searchTerm.toLowerCase()) ||
      product.CategoryName.toLowerCase().includes(searchTerm.toLowerCase());

    const hasMatchingBlocks = product.ProductBlocks.some((block) => {
      if (!statusFilter) return true;
      if (statusFilter === "available") return block.Status === 0;
      return block.Status !== 0;
    });

    return matchesSearch && hasMatchingBlocks;
  });

  return (
    <motion.div
      className="bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border"
      initial={{ opacity: 0, y: 20 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
    >
      <div className="flex justify-between items-center mb-6">
        <div className="flex items-center gap-4">
          <Package size={50} className="text-amber-400" />
          <h2 className="text-4xl font-semibold text-gray-100">Product Inventory</h2>
        </div>

        <div className="flex gap-4">
          <div className="flex gap-2">
            <button
              onClick={() => setStatusFilter(statusFilter === "available" ? null : "available")}
              className={`px-4 py-2 rounded-lg transition-colors ${
                statusFilter === "available"
                  ? "bg-green-500 text-white"
                  : "bg-gray-700 text-gray-300 hover:bg-gray-600"
              }`}
            >
              Available
            </button>
            <button
              onClick={() => setStatusFilter(statusFilter === "more" ? null : "more")}
              className={`px-4 py-2 rounded-lg transition-colors ${
                statusFilter === "more"
                  ? "bg-blue-500 text-white"
                  : "bg-gray-700 text-gray-300 hover:bg-gray-600"
              }`}
            >
              Other
            </button>
          </div>
          <div className="relative">
            <input
              type="text"
              placeholder="Search products..."
              className="bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
            <Search className="absolute left-3 top-2.5 text-gray-400" size={18} />
          </div>
        </div>
      </div>

      {loading ? (
        <div className="flex justify-center items-center h-64">
          <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
        </div>
      ) : (
        <div className="space-y-4">
          {Array.from(new Set(filteredProducts.map((p) => p.CategoryName))).map((category) => (
            <motion.div
              key={category}
              className="bg-gray-800 rounded-lg shadow-sm"
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
            >
              <button
                onClick={() => toggleCategory(category)}
                className="w-full p-4 text-left hover:bg-gray-700 rounded-t-lg flex justify-between items-center"
              >
                <h3 className="text-xl font-semibold text-gray-100">{category}</h3>
                <span className="text-gray-400">
                  {expandedCategories[category] ? "▼" : "▶"}
                </span>
              </button>

              {expandedCategories[category] && (
                <div className="p-4 grid gap-4 md:grid-cols-2 lg:grid-cols-3">
                  {filteredProducts
                    .filter((p) => p.CategoryName === category)
                    .map((product) => (
                      <motion.div
                        key={product.ProductName}
                        className="border border-gray-700 rounded-lg p-4 bg-gray-800"
                        whileHover={{ scale: 1.02 }}
                      >
                        <h4 className="font-bold text-lg mb-4 text-gray-100">
                          {product.ProductName}
                        </h4>

                        {product.ProductBlocks.length === 0 ? (
                          <div className="text-gray-400 text-sm">No inventory available</div>
                        ) : (
                          product.ProductBlocks
                            .filter((block) => {
                              if (!statusFilter) return true;
                              if (statusFilter === "available") return block.Status === 0;
                              return block.Status !== 0;
                            })
                            .map((block) => (
                              <div
                                key={block.ProductBlockId}
                                className="mb-3 last:mb-0 p-3 bg-gray-700 rounded-lg"
                              >
                                <div className="flex justify-between items-center mb-3">
                                  <span className="text-sm font-medium text-amber-400">
                                    Block ID: {block.ProductBlockId}
                                  </span>
                                  <span
                                    className={`px-2 py-1 text-xs font-semibold rounded-full ${getStatusColor(
                                      block.Status
                                    )} text-white max-w-[200px] truncate`}
                                    title={getStatusText(block.Status)}
                                  >
                                    {getStatusText(block.Status)}
                                  </span>
                                </div>

                                <div className="space-y-2 text-sm">
                                  {block.LocationName && (
                                    <div className="flex gap-2 text-gray-300">
                                      <span>Location:</span>
                                      <span className="text-gray-100">{block.LocationName}</span>
                                    </div>
                                  )}

                                  <div className="flex gap-2 text-gray-300">
                                    <span>Quantity:</span>
                                    <span
                                      className={`font-medium ${
                                        block.quantity > 0 ? "text-green-400" : "text-red-400"
                                      }`}
                                    >
                                      {block.quantity}
                                    </span>
                                  </div>

                                  {Object.entries(block.ProductItemIds).length > 0 && (
                                    <div className="mt-2">
                                      <span className="text-gray-300">Item IDs:</span>
                                      <div className="flex flex-wrap gap-1 mt-1">
                                        {Object.keys(block.ProductItemIds).map((id) => (
                                          <span
                                            key={id}
                                            className="px-2 py-1 bg-gray-600 text-gray-100 rounded text-xs"
                                          >
                                            #{id}
                                          </span>
                                        ))}
                                      </div>
                                    </div>
                                  )}
                                </div>
                              </div>
                            ))
                        )}
                      </motion.div>
                    ))}
                </div>
              )}
            </motion.div>
          ))}
        </div>
      )}
    </motion.div>
  );
};

export default ProductsInfo;