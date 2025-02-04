import { useState, useEffect } from 'react';
import { motion } from 'framer-motion';
import { Search, LayoutGrid } from 'lucide-react';

const WarehouseView = () => {
    const [warehouseData, setWarehouseData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        const fetchWarehouseData = async () => {
            try {
                const response = await fetch(
                    'http://localhost:5188/Test/getWarehouseWithLocations?warehouseId=1',
                    { headers: { 'Accept': '/' } }
                );

                if (!response.ok) throw new Error(`HTTP error! status: ${response.status}`);

                const data = await response.json();
                setWarehouseData(data);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchWarehouseData();
    }, []);

    const getStatusStyle = (status) => {
        switch(status) {
            case 0: return 'bg-green-500';
            case 1: return 'bg-red-500';
            case 2: return 'bg-yellow-500';
            default: return 'bg-gray-500';
        }
    };

    const getStatusText = (status) => {
        switch(status) {
            case 0: return 'Available';
            case 1: return 'Out of Stock';
            case 2: return 'Expired';
            default: return 'Unknown';
        }
    };

    const filteredLocations = warehouseData?.locations?.filter(location => {
        const searchLower = searchTerm.toLowerCase();
        return (
            location.LocationName.toLowerCase().includes(searchLower) ||
            location.LocationId.toString().includes(searchTerm)
        );
    });

    if (loading) {
        return (
            <div className="flex justify-center items-center h-screen bg-gray-900">
                <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-blue-500"></div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="flex justify-center items-center h-screen text-red-500 bg-gray-900">
                Error: {error}
            </div>
        );
    }

    return (
        <motion.div
            className="bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border mx-4 my-6"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.4 }}
        >
            <div className="flex justify-between items-center mb-6">
                <div className="flex items-center gap-4">
                    <LayoutGrid size={50} className="text-amber-400" />
                    <h2 className="text-4xl font-semibold text-gray-100">
                        {warehouseData?.warehouseName || 'Warehouse'} Layout
                    </h2>
                </div>

                <div className="relative">
                    <input
                        type="text"
                        placeholder="Search locations..."
                        className="bg-gray-700 text-white placeholder-gray-400 rounded-lg pl-10 pr-4 py-2 focus:outline-none focus:ring-2 focus:ring-blue-500 w-64"
                        value={searchTerm}
                        onChange={(e) => setSearchTerm(e.target.value)}
                    />
                    <Search className="absolute left-3 top-2.5 text-gray-400" size={18} />
                </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-4">
                {filteredLocations?.map(location => (
                    <motion.div
                        key={location.LocationId}
                        className="border border-gray-700 rounded-lg p-4 bg-gray-800 relative group"
                        whileHover={{ scale: 1.02 }}
                        transition={{ type: "spring", stiffness: 300 }}
                    >
                        {/* Location header */}
                        <div className="flex justify-between items-center mb-4">
                            <h3 className="text-lg font-semibold text-gray-100">
                                {location.LocationName}
                            </h3>
                            <span className={`text-xs px-2 py-1 rounded-full ${
                                location.isEmpty
                                    ? 'bg-gray-700 text-gray-400'
                                    : 'bg-green-900/30 text-green-400'
                            }`}>
                                {location.isEmpty ? 'Vacant' : 'Occupied'}
                            </span>
                        </div>

                        {location.isEmpty ? (
                            <div className="flex flex-col items-center justify-center h-40">
                                <div className="text-4xl mb-2 text-gray-500">📭</div>
                                <p className="text-gray-400 text-sm font-medium">
                                    Empty Location
                                </p>
                                <p className="text-xs text-gray-500 mt-1">
                                    Available for storage
                                </p>
                            </div>
                        ) : (
                            <div className="space-y-3">
                                {location.Block ? (
                                    <>
                                        <div className="flex items-center justify-between">
                                            <span className="text-sm text-gray-300">Product:</span>
                                            <span className="text-sm font-medium text-amber-400">
                                                {location.Block.productName}
                                            </span>
                                        </div>

                                        <div className="flex items-center justify-between">
                                            <span className="text-sm text-gray-300">Status:</span>
                                            <span className={`px-2 py-1 text-xs font-semibold rounded-full ${getStatusStyle(location.Block.Status)}`}>
                                                {getStatusText(location.Block.Status)}
                                            </span>
                                        </div>

                                        <div className="flex items-center justify-between">
                                            <span className="text-sm text-gray-300">Quantity:</span>
                                            <span className={`text-sm ${
                                                location.Block.quantity > 0
                                                    ? 'text-green-400'
                                                    : 'text-red-400'
                                            }`}>
                                                {location.Block.quantity}
                                            </span>
                                        </div>

                                        {Object.keys(location.Block.ProductItemIds).length > 0 && (
                                            <div className="mt-3">
                                                <span className="text-sm text-gray-300">Item IDs:</span>
                                                <div className="flex flex-wrap gap-1 mt-1">
                                                    {Object.keys(location.Block.ProductItemIds).map(id => (
                                                        <span
                                                            key={id}
                                                            className="px-2 py-1 bg-gray-700 text-gray-100 rounded text-xs"
                                                        >
                                                            #{id}
                                                        </span>
                                                    ))}
                                                </div>
                                            </div>
                                        )}
                                    </>
                                ) : (
                                    <div className="text-center py-2">
                                        <span className="text-yellow-500 text-sm">
                                            ⚠️ No product information available
                                        </span>
                                    </div>
                                )}
                            </div>
                        )}
                    </motion.div>
                ))}
            </div>
        </motion.div>
    );
};

export default WarehouseView;