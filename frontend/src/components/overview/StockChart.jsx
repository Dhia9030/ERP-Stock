import { useState, useEffect } from "react";
import { motion } from "framer-motion";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';
import { Bar } from "react-chartjs-2";
import './StockChart.css';
ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

const products = {
  furniture: [
    { name: "Sofa", price: 100, stock: [10, 8, 6] },
    { name: "Chair", price: 50, stock: [15, 12, 10] },
    { name: "Table", price: 150, stock: [5, 4, 3] },
    { name: "Bed", price: 200, stock: [7, 6, 5] },
  ],
  electronics: [
    { name: "Smartphone", price: 500, stock: [20, 18, 15] },
    { name: "Laptop", price: 800, stock: [10, 9, 8] },
    { name: "Tablet", price: 300, stock: [25, 22, 20] },
    { name: "Smartwatch", price: 200, stock: [30, 28, 25] },
  ],
  clothing: [
    { name: "Shirt", price: 20, stock: [50, 45, 40] },
    { name: "Pants", price: 30, stock: [40, 35, 30] },
    { name: "Dress", price: 40, stock: [30, 28, 25] },
    { name: "Jacket", price: 50, stock: [20, 18, 15] },
  ],
  food: [
    { name: "Bread", price: 5, stock: [100, 90, 80] },
    { name: "Milk", price: 2, stock: [200, 180, 160] },
    { name: "Eggs", price: 3, stock: [150, 140, 130] },
    { name: "Cheese", price: 4, stock: [120, 110, 100] },
  ],
  beauty: [
    { name: "Shampoo", price: 10, stock: [60, 55, 50] },
    { name: "Soap", price: 5, stock: [80, 75, 70] },
    { name: "Perfume", price: 20, stock: [40, 35, 30] },
    { name: "Lotion", price: 15, stock: [50, 45, 40] },
  ],
};

const allProducts = Object.values(products).flatMap(category => category);

const StockChart = ({ selectedCategory }) => {
  const [selectedProduct, setSelectedProduct] = useState(
    selectedCategory === 'all' ? allProducts[0].name : products[selectedCategory][0].name
  );

  useEffect(() => {
    setSelectedProduct(
      selectedCategory === 'all' ? allProducts[0].name : products[selectedCategory][0].name
    );
  }, [selectedCategory]);

  const handleProductChange = (event) => {
    setSelectedProduct(event.target.value);
  };

  const filteredProducts = selectedCategory === 'all' ? allProducts : products[selectedCategory];
  const productData = filteredProducts.find(product => product.name === selectedProduct);

  if (!productData) {
    return <div className="no-product"></div>;
  }

  const chartData = {
    labels: ['Month 1', 'Month 2', 'Month 3'],
    datasets: [
      {
        label: selectedProduct,
        data: productData.stock,
        backgroundColor: productData.stock.map(value => value < 50 ? 'rgba(255, 99, 132, 0.2)' : 'rgba(75, 192, 192, 0.2)'),
        borderColor: productData.stock.map(value => value < 50 ? 'rgba(255, 99, 132, 1)' : 'rgba(75, 192, 192, 1)'),
        borderWidth: 1,
        barThickness: 20, // Adjust this value to make the bars slimmer
        borderRadius: 10,
      },
    ],
  };

  const chartOptions = {
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
        labels: {
          color: '#ffffff',
          font: {
            family: 'Montserrat', // Change title font family
          }, // Change legend text color to white
        },
      },
      title: {
        display: true,
        text: 'Products in Stock Over 3 Months',
        color: '#ffffff',
        font: {
          family: 'Montserrat', // Change title font family
          size: 35,
        },
      },
      tooltip: {
        bodyColor: '#ffffff',
        titleFont: {
          family: 'Montserrat', // Change tooltip title font family
        },
        bodyFont: {
          family: 'Montserrat', // Change tooltip body font family
        },
      },
    },
    scales: {
      x: {
        ticks: {
          color: '#ffffff', // Change x-axis text color to white
          font: {
            family: 'Montserrat', // Change x-axis font family
          },
        },
      },
      y: {
        ticks: {
          color: '#ffffff', // Change y-axis text color to white
          font: {
            family: 'Montserrat', // Change x-axis font family
          },
        },
      },
    },
  };

  return (
    <motion.div
			className='flex justify-center min-h-[500px] min-w-max p-12 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl  border '
			initial={{ opacity: 0, y: 0 }}
			animate={{ opacity: 1, y: 0 }}
			transition={{ duration: 0.4 }}
		>
    <div className="stock-chart">
      <div className="filters">
        <label className="filter  pb-6">
          <span className="text-4xl">Product:</span>
          <select className=" dropdown text-2xl text-center text-sky-600" value={selectedProduct} onChange={handleProductChange}>
            {filteredProducts.map(product => (
              <option className="option text-start " key={product.name} value={product.name}>
                {product.name}
              </option>
            ))}
          </select>
        </label>
      </div>
      <Bar data={chartData} options={chartOptions} />
    </div>
    </motion.div>
  );
};

export default StockChart;