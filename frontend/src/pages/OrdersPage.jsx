import React, { useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import Header from "../components/common/Header";
import DailyOrders from "../components/orders/DailyOrders";
import OrderDistribution from "../components/orders/OrderDistribution";
import OrdersTable from "../components/orders/OrdersTable";
import CustomerOrder from './CustomerOrder';
import {useOrder} from "../context/OrderProvider"


const OrdersPage = () => {
    
    

    return (
        <div className='flex-1 relative z-10 overflow-auto'>
            <Header title={"Orders"} />
            <main className='max-w-7xl mx-auto py-6 px-4 lg:px-8'>
                    <OrdersTable  />
                <div className='mt-3 grid grid-cols-1 lg:grid-cols-2 gap-8 mb-8'>
                    <DailyOrders />
                    <OrderDistribution />
                </div>
            </main>
        </div>
    );
};

export default OrdersPage;