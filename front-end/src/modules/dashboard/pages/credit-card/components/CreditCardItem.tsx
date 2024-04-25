import React from 'react'

const CreditCardItem = () => {
  return (
    <div className='pt-5 pb-[.15rem] flex'>
        <div className='flex flex-none bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-4 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'>
        <div className='grid grid-cols-3 gap-2'>
            <span className='flex items-center justify-center'>142.700.667-92 - Fernando Calheiros Areias</span>
            <span className='flex items-center justify-center'>**** **** **** 8257</span>
            <span className='flex items-center justify-center'>Active</span>
        </div>
        </div>
    </div>
  )
}

export default CreditCardItem