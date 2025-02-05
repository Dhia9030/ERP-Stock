import { useState, useEffect, useMemo } from "react";
import { motion } from "framer-motion";
import { Chart as ChartJS, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend } from 'chart.js';
import { Bar } from "react-chartjs-2";
import './StockChart.css';

ChartJS.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

const StockChart = ({ selectedCategory }) => {
  const [productsData, setProductsData] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [selectedProduct, setSelectedProduct] = useState(null);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('https://localhost:7073/Test/PredictionForAllCategories');
        if (!response.ok) throw new Error('Network response was not ok');
        const data = await response.json();
        
        const transformedData = data.reduce((acc, category) => {
          const key = category.categoryName.toLowerCase();
          acc[key] = category.predectionDtos.map(p => ({
            name: p.productName,
            stock: p.stockQuantity
          }));
          return acc;
        }, {});

        setProductsData(transformedData);
        setLoading(false);
      } catch (err) {
        setError(err.message);
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const allProducts = useMemo(() => {
    if (!productsData) return [];
    return Object.values(productsData).flatMap(category => category);
  }, [productsData]);

  useEffect(() => {
    if (productsData) {
      const categoryProducts = selectedCategory === 'all' 
        ? allProducts 
        : productsData[selectedCategory] || [];
      
      if (categoryProducts.length > 0) {
        setSelectedProduct(categoryProducts[0].name);
      }
    }
  }, [selectedCategory, productsData, allProducts]);

  const handleProductChange = (event) => {
    setSelectedProduct(event.target.value);
  };

  if (loading) {
    return (
      <motion.div
        className="flex justify-center items-center h-64 bg-gradient-to-br from-sky-800 to-sky-900 rounded-xl"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
      >
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
      </motion.div>
    );
  }

  if (error) {
    return (
      <motion.div
        className="flex justify-center items-center h-64 bg-gradient-to-br from-sky-800 to-sky-900 rounded-xl text-red-400"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
      >
        Error: {error}
      </motion.div>
    );
  }

  const filteredProducts = selectedCategory === 'all' 
    ? allProducts 
    : productsData[selectedCategory] || [];

  const productData = filteredProducts.find(product => product.name === selectedProduct);

  if (!productData) {
    return (
      <motion.div
        className="flex justify-center items-center h-64 bg-gradient-to-br from-sky-800 to-sky-900 rounded-xl text-gray-300"
        initial={{ opacity: 0 }}
        animate={{ opacity: 1 }}
      >
        No product data available
      </motion.div>
    );
  }

  const chartData = {
    labels: ['Day1', 'Day2', 'Day3', 'Day4', 'Day5', 'Day6', 'Day7'],
    datasets: [
      {
        label: selectedProduct,
        data: productData.stock,
        backgroundColor: productData.stock.map(value => 
          value < 0 ? 'rgba(255, 99, 132, 0.2)' : 'rgba(75, 192, 192, 0.2)'
        ),
        borderColor: productData.stock.map(value => 
          value < 0 ? 'rgba(255, 99, 132, 1)' : 'rgba(75, 192, 192, 1)'
        ),
        borderWidth: 1,
        barThickness: 20,
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
            family: 'Montserrat',
          },
        },
      },
      title: {
        display: true,
        text: 'Stock Prediction for Next 7 Days',
        color: '#ffffff',
        font: {
          family: 'Montserrat',
          size: 35,
        },
      },
      tooltip: {
        bodyColor: '#ffffff',
        titleFont: {
          family: 'Montserrat',
        },
        bodyFont: {
          family: 'Montserrat',
        },
      },
    },
    scales: {
      x: {
        ticks: {
          color: '#ffffff',
          font: {
            family: 'Montserrat',
          },
        },
      },
      y: {
        ticks: {
          color: '#ffffff',
          font: {
            family: 'Montserrat',
          },
          callback: function(value) {
            if (value % 1 === 0) {
              return value;
            }
          },
          precision: 0,
        },
        
        stepSize: 1
      },
    },
  };

  return (
    <motion.div
      className="flex justify-center min-h-[500px] min-w-max p-12 bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl border"
      initial={{ opacity: 0, y: 0 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.4 }}
    >
      <div className="stock-chart">
        <div className="filters">
          <label className="filter pb-6">
            <span className="text-4xl">Product:</span>
            <select 
              className="dropdown text-2xl text-center text-sky-600" 
              value={selectedProduct} 
              onChange={handleProductChange}
            >
              {filteredProducts.map(product => (
                <option 
                  className="option text-start" 
                  key={product.name} 
                  value={product.name}
                >
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