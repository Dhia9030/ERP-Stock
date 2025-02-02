import React, { useState } from 'react';
import { useTransfer } from '../../context/TransferProvider';

const InternalTransferTable = () => {
  const { useInternal } = useTransfer();
  const internalTransfers = useInternal();
  const [expandedRow, setExpandedRow] = useState(null);
  const [showAll, setShowAll] = useState(false);

  const handleRowClick = (index) => {
    setExpandedRow(expandedRow === index ? null : index);
  };

  const handleShowAllClick = () => {
    setShowAll(!showAll);
  };

  const displayedTransfers = showAll ? internalTransfers : internalTransfers.slice(0, 7);

  return (
    <div className='flex-1 flex-col items-center justify-center min-h-96 w-full ml-7 '>
      <div className='relative space-y-4 m-7 w-full max-w-6xl bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border'>
        <h1 className='text-center text-4xl font-bold text-white mb-4'>Internal Transfers</h1>
        <div className='overflow-x-auto'>
          <table className='min-w-full divide-y divide-gray-700'>
            <thead>
              <tr>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Transfer ID
                </th>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Quantity
                </th>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Date
                </th>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Product Name
                </th>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Category Name
                </th>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Source Location
                </th>
                <th className='px-3 py-2 text-left text-lg font-semibold text-green-500 uppercase tracking-wider'>
                  Destination Location
                </th>
              </tr>
            </thead>
            <tbody className='divide-y divide-gray-700'>
              {displayedTransfers.map((transfer, index) => (
                <React.Fragment key={transfer.id}>
                  <tr
                    className='hover:bg-gray-800 cursor-pointer'
                    onClick={() => handleRowClick(index)}
                  >
                    <td className='px-1 py-2 whitespace-normal text-lg font-medium text-gray-100'>
                      {transfer.id} <span className='text-sm font-thin'>click for details</span>
                    </td>
                    <td className='px-3 py-2 whitespace-normal text-lg text-gray-300'>
                      {transfer.quantity}
                    </td>
                    <td className='px-3 py-2 whitespace-normal text-lg text-gray-300'>
                      {transfer.date}
                    </td>
                    <td className='px-3 py-2 whitespace-normal text-lg text-gray-300'>
                      {transfer.productName}
                    </td>
                    <td className='px-3 py-2 whitespace-normal text-lg text-gray-300'>
                      {transfer.categoryName}
                    </td>
                    <td className='px-3 py-2 whitespace-normal text-lg text-gray-300'>
                      {transfer.sourceLocation}
                    </td>
                    <td className='px-3 py-2 whitespace-normal text-lg text-gray-300'>
                      {transfer.destinationLocation}
                    </td>
                  </tr>
                  {expandedRow === index && (
                    <tr>
                      <td colSpan="7" className="px-3 py-2">
                        <div className="bg-gray-800 p-4 rounded-lg">
                          <h3 className="text-lg font-bold mb-2 text-white">Product Item IDs</h3>
                          <p className="text-gray-100">{transfer.productItemIds.join(', ')}</p>
                        </div>
                      </td>
                    </tr>
                  )}
                </React.Fragment>
              ))}
            </tbody>
          </table>
          {internalTransfers.length > 7 && (
            <div className="text-center mt-4">
              <button
                onClick={handleShowAllClick}
                className="px-4 py-2 bg-blue-400 text-white rounded-lg"
              >
                {showAll ? 'Show Less' : 'See All'}
              </button>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default InternalTransferTable;