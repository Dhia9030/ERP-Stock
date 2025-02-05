import { motion } from "framer-motion";
import Header from "../components/common/Header";
import StatCard from "../components/common/StatCard";
import { AlertTriangle, Package } from "lucide-react";
import CategoryDistributionChart from "../components/overview/CategoryDistributionChart";
import ProductsTable from "../components/products/ProductsTable";
import Category from "../components/overview/Category";
import { useState, useEffect } from "react";
import { useProducts } from "../context/ProductProvider";

const ProductsPage = () => {
  const [selectedList, setSelectedList] = useState("all");
  const [lowproducts, setLowproducts] = useState([]);
  const { products } = useProducts();

  const ClothingMin = 100;
  const ElectronicsMin = 40;
  const FoodMin = 75;

  const [productsLength, setProductsLength] = useState(0);
  const [lowStockLength, setLowStockLength] = useState(0);

  useEffect(() => {
    if (products.length > 0) {
      setProductsLength(products.length);
    }
  }, [products]);

  useEffect(() => {
    if (products && products.length > 0) {
      const lowStockProducts = products.filter((product) => {
        if (product.category === "Clothing") {
          return product.stock < ClothingMin;
        } else if (product.category === "Electronics") {
          return product.stock < ElectronicsMin;
        } else if (product.category === "Food") {
          return product.stock < FoodMin;
        }
        return false;
      });

      setLowproducts(lowStockProducts);
      setLowStockLength(lowStockProducts.length);
    }
  }, [products]);

  return (
    <div className="flex-1 overflow-auto relative z-10">
      <Header title="Products" />

      <main className="max-w-7xl mx-auto py-6 px-4 lg:px-8">
        {/* STATS */}
        <motion.div
          className="flex justify-center items-center gap-10 mb-8"
          initial={{ opacity: 0, y: 0 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
        >
          <StatCard
            name="Total Products"
            icon={Package}
            value={productsLength}
            color="#6366F1"
          />
          <StatCard
            name="Low Stock"
            icon={AlertTriangle}
            value={lowStockLength}
            color="#F59E0B"
          />
        </motion.div>
        <Category
          selectedCategory={selectedList}
          setSelectedCategory={setSelectedList}
        />

        <ProductsTable selectedList={selectedList} />

        {/* CHARTS */}
        <div className="grid grid-col-1 lg:grid-cols-2 gap-8">
          <CategoryDistributionChart />
        </div>
      </main>
    </div>
  );
};

export default ProductsPage;