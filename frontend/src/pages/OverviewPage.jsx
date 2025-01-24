import { BarChart2, ShoppingBag, Users, Zap } from "lucide-react";
import { motion } from "framer-motion";
import { useState, useEffect , useRef} from "react";
import { useProducts } from "../ProductProvider";
import {toast} from "react-toastify";

import Header from "../components/common/Header";
import StatCard from "../components/common/StatCard";
import SalesOverviewChart from "../components/overview/SalesOverviewChart";
import CategoryDistributionChart from "../components/overview/CategoryDistributionChart";
import SalesChannelChart from "../components/overview/SalesChannelChart";
import Category from "../components/overview/Category";
import StockChart from "../components/overview/StockChart";

const OverviewPage = () => {

//notification
// {const PRODUCT_DATA = useProducts();


  const [selectedCategory, setSelectedCategory] = useState("all");

  // 

  return (
    <div className='flex-1 align-middle overflow-auto relative z-10'>
      <Header title='Overview' />

      <main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
        {/* STATS */}
        <motion.div
          className='flex justify-center align-center gap-4 '
          initial={{ opacity: 0, y: 0 }}
          animate={{ opacity: 1, y: 0 }}
          transition={{ duration: 0.5 }}
        >
          <div className="mr-52 h-96 flex flex-col justify-center align-center gap-4">
          <StatCard name='Total Sales' icon={Zap} value='$12,345' color='#6366F1' />
          <StatCard name='Total Products' icon={ShoppingBag} value='567' color='#EC4899' />
          </div>
          
          <Category
            selectedCategory={selectedCategory}
            setSelectedCategory={setSelectedCategory} 
          />
        </motion.div>

        {/* CHARTS */}
        
          <div className="grid grid-cols-1 gap-5">
          <StockChart selectedCategory={selectedCategory} />
          
          <CategoryDistributionChart />
          <SalesChannelChart />
          </div>
      </main>
    </div>
  );
};

export default OverviewPage;