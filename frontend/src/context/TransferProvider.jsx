import React, { createContext, useContext, useState, useEffect } from 'react';
import { getToken } from '../utility/storage';
const TransferContext = createContext();

export const TransferProvider = ({ children }) => {
  const [transfers, setTransfers] = useState([]);
  const [importTransfers, setImportTransfers] = useState([]);
  const [exportTransfers, setExportTransfers] = useState([]);
  const [deletedTransfers, setDeletedTransfers] = useState([]);
  const [internalTransfers, setInternalTransfers] = useState([]);
  const [mergeTransfers, setMergeTransfers] = useState([]);

  useEffect(() => {
    const fetchTransfers = async () => {
      const token = getToken();
      if (!token) {
        console.log('User is not authenticated. Skipping fetch.');
        return;
      }

      try {
        const response = await fetch('http://localhost:5188/Test/get all stock movement', {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        // Format the data as needed
        const formattedData = data.map(transfer => ({
          id: transfer.StockMovementId,
          createdBy: transfer.Createdby,
          quantity: transfer.Quantity,
          date: new Date(transfer.MovementDate).toLocaleDateString(),
          type: transfer.MovementType,
          productName: transfer.ProductName,
          categoryName: transfer.CategoryName,
          sourceLocation: transfer.SourceLocationName,
          destinationLocation: transfer.DestinationLocationName,
          productItemIds: transfer.productItemIds
        }));
        setTransfers(formattedData);
        console.log('Transfers:', formattedData);
      } catch (error) {
        console.error('Error fetching transfers:', error);
      }
    };

    fetchTransfers();
  }, []);

  useEffect(() => {
    setImportTransfers(transfers.filter(transfer => transfer.type === 0));
    setExportTransfers(transfers.filter(transfer => transfer.type === 1));
    setDeletedTransfers(transfers.filter(transfer => transfer.type === 4));
    setInternalTransfers(transfers.filter(transfer => transfer.type === 2));
    setMergeTransfers(transfers.filter(transfer => transfer.type === 3));
  }, [transfers]);

  const useImport = () => {
    return importTransfers;
  };

  const useExport = () => {
    return exportTransfers;
  };

  const useDeleted = () => {
    return deletedTransfers;
  };

  const useInternal = () => {
    return internalTransfers;
  };

  const useMerge = () => {
    return mergeTransfers;
  };

  return (
    <TransferContext.Provider value={{ transfers, useImport, useExport, useDeleted, useInternal, useMerge }}>
      {children}
    </TransferContext.Provider>
  );
};

export const useTransfer = () => {
  return useContext(TransferContext);
};