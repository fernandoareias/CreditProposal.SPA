import React from 'react'
import { Outlet } from 'react-router-dom'
import Search from '../../../../core/components/Search'
import CreditCardItem from './components/CreditCardItem'

const CreditCardPage = () => {
  return (
    <>
    <section className=''>
    <div className="flex shrink-0 px-8 items-center justify-between h-[96px]">
            <div>

            </div>
            <div>
              
              
            </div>
          </div>
      <Search placeholder='CPF'/>

      <div className='relative pl-3 my-5' style={{ maxHeight: 'calc(100vh - 15rem)', overflowY: 'auto' }}>
          <div className='flex flex-col w-full font-medium'>
            <CreditCardItem/>
            <CreditCardItem/>
            <CreditCardItem/>
            <CreditCardItem/>
            <CreditCardItem/>
          </div>
        </div>


    </section>
  </>
  )
}

export default CreditCardPage