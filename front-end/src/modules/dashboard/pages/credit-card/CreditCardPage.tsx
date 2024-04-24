import React from 'react'
import { Outlet } from 'react-router-dom'
import Search from '../../../../core/components/Search'

const CreditCardPage = () => {
  return (
    <>
    <section className=''>
      <Search placeholder='Document'/>
    </section>
  </>
  )
}

export default CreditCardPage