import React, { useState, useEffect } from 'react';
import { AlertTriangle, Search } from 'lucide-react';
import * as SignalR from '@microsoft/signalr';

const lowproducts = [
  {
    name: "Phone",
    quantity: 5,
    manufacturer: "Microsoft",
    category: "Electronics"
  },
  {
    name: "Laptop",
    quantity: 10,
    manufacturer: "Apple",
    category: "Electronics"
  }
];

const LowStockTable = () => {
  const [searchTerm, setSearchTerm] = useState("");
  const [lowproducts, setLowStockProducts] = useState([]);
  const [filteredProducts, setFilteredProducts] = useState(lowproducts);

  useEffect(() => {
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:5001/stockhub")
      .configureLogging(SignalR.LogLevel.Information)
      .build();

    connection.start()
      .then(() => {
        console.log("Connected to SignalR hub for low stock products");

        connection.on("ReceiveLowStockProducts", (data) => {
          console.log("Received low stock products:", data);
          setLowStockProducts(data);
          setFilteredProducts(data);
        });

        connection.invoke("GetLowStockProducts")
          .catch(err => console.error(err.toString()));
      })
      .catch(err => console.error("Error connecting to SignalR hub:", err));

    return () => {
      connection.stop().then(() => console.log("Disconnected from SignalR hub"));
    };
  }, []);



  useEffect(() => {
    const filtered = lowproducts.filter((product) =>
      product.name.toLowerCase().includes(searchTerm) ||
      product.manufacturer.toLowerCase().includes(searchTerm) ||
      product.category.toLowerCase().includes(searchTerm)
    );
    setFilteredProducts(filtered);
  }, [searchTerm]);

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
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Manufacturer</th>
              <th className="px-6 py-3 text-left text-sm font-medium uppercase">Category</th>
            </tr>
          </thead>
          <tbody>
            {filteredProducts.map((product, index) => (
              <tr key={index} className="cursor-pointer text-gray-100 hover:bg-gray-800 border-b-[1px] border-gray-700">
                <td className="flex items-center px-6 py-4 text-sm">
                  <img
                    src="https://images.unsplash.com/photo-1627989580309-bfaf3e58af6f?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8d2lyZWxlc3MlMjBlYXJidWRzfGVufDB8fDB8fHww"
                    alt="Product img"
                    className="w-10 h-10 rounded-full mr-4"
                  />
                  {product.name}
                </td>
                <td className="px-6 py-4 text-sm">{product.quantity}</td>
                <td className="px-6 py-4 text-sm">{product.manufacturer}</td>
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