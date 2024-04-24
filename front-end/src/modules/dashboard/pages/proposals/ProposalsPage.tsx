import React, { useState } from 'react'
import { Outlet } from 'react-router-dom'
import Search from '../../../../core/components/Search'
import ProposalsItem from './components/ProposalsItem'

const ProposalsPage = () => {
 
  
  return (
    <>
      <section className=''>
        <Search placeholder='CPF'/>
        

        <div className='relative pl-3 my-5' style={{ maxHeight: 'calc(100vh - 15rem)', overflowY: 'auto' }}>
          <div className='flex flex-col w-full font-medium'>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
          </div>
        </div>
      </section>
    </>
  )
}

export default ProposalsPage