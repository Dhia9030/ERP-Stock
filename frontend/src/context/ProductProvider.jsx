import {createContext , useContext , useEffect , useState} from 'react';
import * as SignalR from '@microsoft/signalr';
import useSignalR from '../SignalR';
import { getToken } from '../utility/storage';

const productContext = createContext();

const ProductProvider=({children})=>{
    const [products, setProducts] = useState([]);
    const [token, setToken] = useState(getToken());
    useEffect(() => {
        const fetchProducts = async () => {
                  if (!token) {
                    console.log('User is not authenticated. Skipping fetch.');
                    return;
                  }
            try {
                const response = await fetch('http://localhost:5188/Test/getAllProduct',{
                    headers: {
                        'Authorization': `Bearer ${token}`
                      }
                }); 
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const data = await response.json();
                const mappedProducts = data.map(product => ({
                    id: product.ProductId,
                    name: product.Name,
                    category: product.Category ? product.Category.Name : 'N/A',
                    price: product.Price,
                    stock: product.StockQuantity,
                    sales: product.SalesQuantity,
                }));

                setProducts(mappedProducts);
            } catch (error) {
                console.error('Error fetching products:', error);
            }
        };

        fetchProducts();
    }, [token]);

    const updateToken = (newToken) => {
        setToken(newToken);
    };

        

    // const products = [
    //   { id: 1, name: "Wireless Earbuds", category: "Electronics", price: 59.99, stock: 0, sales: 1200 },
    //   { id: 2, name: "Leather Wallet", category: "beauty", price: 39.99, stock: 89, sales: 800 },
    //   { id: 3, name: "Smart Watch", category: "Electronics", price: 199.99, stock: 3, sales: 650 },
    //   { id: 4, name: "Yoga Mat", category: "clothing", price: 29.99, stock: 4, sales: 950 },
    //   { id: 5, name: "Coffee Maker", category: "furniture", price: 79.99, stock: 78, sales: 720 },
    // ];

    return(
        <productContext.Provider value={{products , updateToken}}>
            {children}
        </productContext.Provider>
    )

}

const useProducts=()=>{
    return useContext(productContext);
}

export {ProductProvider, useProducts};