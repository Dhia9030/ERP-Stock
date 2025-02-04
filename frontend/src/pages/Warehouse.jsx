import React from 'react';
import Header from '../components/common/Header';
import WarehouseView from "../components/warehouse/Warehouse.jsx";

const Warehouses = () => {
    return (
        <div className="flex-1 overflow-auto relative z-10">
            <Header title="Warehouse Information" />
            <div className="p-4">
                <WarehouseView />
            </div>
        </div>
    );
};

export default Warehouses;