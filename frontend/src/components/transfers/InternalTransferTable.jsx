import React from 'react';
import { useTransfer } from '../../context/TransferProvider';

const InternalTransferTable = () => {
  const { useInternal } = useTransfer();
  const internalTransfers = useInternal();
  //console.log('Internal Transfers:', internalTransfers);
  


  return (
    <div className='flex-1 flex-col items-center justify-center min-h-96 w-full ml-7 '>
      <div className='relative space-y-4 m-7 w-full max-w-6xl bg-gradient-to-br from-sky-800 to-sky-900 bg-opacity-50 backdrop-blur-md shadow-lg rounded-xl p-6 border'>
        <h1 className='text-center text-4xl font-bold text-white mb-4'>Internal Transfers</h1>
        <div className='overflow-x-auto'>
          <table className='min-w-full divide-y divide-gray-700'>
            <thead>
              <tr>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Transfer ID
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Created By
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Quantity
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Date
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Product Name
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Category Name
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Source Location
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Destination Location
                </th>
                <th className='px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider'>
                  Product Item IDs
                </th>
              </tr>
            </thead>
            <tbody className='divide-y divide-gray-700'>
              {internalTransfers.map((transfer) => (
                <tr key={transfer.id} className='hover:bg-gray-800'>
                  <td className='px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-100'>
                    {transfer.id}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.createdBy}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.quantity}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.date}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.productName}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.categoryName}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.sourceLocation}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.destinationLocation}
                  </td>
                  <td className='px-6 py-4 whitespace-nowrap text-sm text-gray-300'>
                    {transfer.productItemIds.join(', ')}
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default InternalTransferTable;