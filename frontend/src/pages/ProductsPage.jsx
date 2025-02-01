import { motion } from "framer-motion";

import Header from "../components/common/Header";
import StatCard from "../components/common/StatCard";

import { AlertTriangle, DollarSign, Package, TrendingUp } from "lucide-react";
import CategoryDistributionChart from "../components/overview/CategoryDistributionChart";
import ProductsTable from "../components/products/ProductsTable";
import Category from "../components/overview/Category";
import { useState , useEffect} from "react";
import { useProducts } from "../context/ProductProvider";

const ProductsPage = () => {

	

	const [selectedList, setSelectedList] = useState("all");
	const PRODUCT_DATA = useProducts();
	
	const [productsLength, setProductsLength] = useState(0);
	useEffect(() => {
		if (PRODUCT_DATA.length > 0) {
			setProductsLength(PRODUCT_DATA.length);
		}
	}, [PRODUCT_DATA]);
	console.log('mawjouda', productsLength);
	return (
		<div className='flex-1 overflow-auto relative z-10'>
			<Header  title='Products' />

			<main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
				{/* STATS */}
				<motion.div
					className='grid grid-cols-1 gap-5 sm:grid-cols-2 lg:grid-cols-4 mb-8'
					initial={{ opacity: 0, y: 0 }}
					animate={{ opacity: 1, y: 0 }}
					transition={{ duration: 0.5 }}
				>
					<StatCard name='Total Products' icon={Package} value={productsLength} color='#6366F1' />
					<StatCard name='Top Selling' icon={TrendingUp} value={89} color='#10B981' />
					<StatCard name='Low Stock' icon={AlertTriangle} value={23} color='#F59E0B' />
					<StatCard name='Total Revenue' icon={DollarSign} value={"$543,210"} color='#EF4444' />
				</motion.div>
				< Category selectedCategory={selectedList} setSelectedCategory={setSelectedList}/>

				<ProductsTable selectedList = {selectedList} />

				{/* CHARTS */}
				<div className='grid grid-col-1 lg:grid-cols-2 gap-8'>
					
					<CategoryDistributionChart />
				</div>
			</main>
		</div>
	);
};
export default ProductsPage;
