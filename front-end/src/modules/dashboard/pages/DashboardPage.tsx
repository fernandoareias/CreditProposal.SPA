import React from 'react';
import { Link } from 'react-router-dom';
import { Outlet } from 'react-router-dom';

const DashboardPage = () => {
  return (
    <div className="grid grid-cols-1 lg:grid-cols-4 gap-4">
      <aside className="bg-gray-900 text-white h-screen lg:col-span-1 border-r border-r-dashed border-r-neutral-200 shadow transition-all duration-300 ease-in-out" id="sidenav-main">
        <div className="flex shrink-0 px-8 items-center justify-between h-[96px]">
        
        </div>

        <div className="hidden border-b border-dashed lg:block dark:border-neutral-700/70 border-neutral-200"></div>

        <div className="flex items-center justify-between px-8 py-5">
          <div className="flex items-center mr-5">
            <div className="mr-5">
              <div className="inline-block relative shrink-0 cursor-pointer rounded-[.95rem]">
                <img className="w-[40px] h-[40px] shrink-0 inline-block rounded-[.95rem]" src="https://raw.githubusercontent.com/Loopple/loopple-public-assets/main/riva-dashboard-tailwind/img/avatars/avatar1.jpg" alt="avatar image" />
              </div>
            </div>
            <div className="mr-2 ">
              <a href="javascript:void(0)" className="dark:hover:text-primary hover:text-primary transition-colors duration-200 ease-in-out text-[1.075rem] font-medium dark:text-slate-50 hover:text-slate-300">Robert Jason</a>
              <span className="text-secondary-dark dark:text-stone-500 font-medium block text-[0.85rem]">SEO Manager</span>
            </div>
          </div>
          <a className="inline-flex relative items-center group justify-end text-base font-medium leading-normal text-center align-middle cursor-pointer rounded-[.95rem] transition-colors duration-150 ease-in-out text-dark bg-transparent shadow-none border-0" href="javascript:void(0)">
            <span className="leading-none transition-colors duration-200 ease-in-out peer shrink-0 group-hover:text-primary text-secondary-dark">
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" stroke-width="1.5" stroke="currentColor" className="w-6 h-6 text-white">
              <path stroke-linecap="round" stroke-linejoin="round" d="M10.343 3.94c.09-.542.56-.94 1.11-.94h1.093c.55 0 1.02.398 1.11.94l.149.894c.07.424.384.764.78.93.398.164.855.142 1.205-.108l.737-.527a1.125 1.125 0 0 1 1.45.12l.773.774c.39.389.44 1.002.12 1.45l-.527.737c-.25.35-.272.806-.107 1.204.165.397.505.71.93.78l.893.15c.543.09.94.559.94 1.109v1.094c0 .55-.397 1.02-.94 1.11l-.894.149c-.424.07-.764.383-.929.78-.165.398-.143.854.107 1.204l.527.738c.32.447.269 1.06-.12 1.45l-.774.773a1.125 1.125 0 0 1-1.449.12l-.738-.527c-.35-.25-.806-.272-1.203-.107-.398.165-.71.505-.781.929l-.149.894c-.09.542-.56.94-1.11.94h-1.094c-.55 0-1.019-.398-1.11-.94l-.148-.894c-.071-.424-.384-.764-.781-.93-.398-.164-.854-.142-1.204.108l-.738.527c-.447.32-1.06.269-1.45-.12l-.773-.774a1.125 1.125 0 0 1-.12-1.45l.527-.737c.25-.35.272-.806.108-1.204-.165-.397-.506-.71-.93-.78l-.894-.15c-.542-.09-.94-.56-.94-1.109v-1.094c0-.55.398-1.02.94-1.11l.894-.149c.424-.07.765-.383.93-.78.165-.398.143-.854-.108-1.204l-.526-.738a1.125 1.125 0 0 1 .12-1.45l.773-.773a1.125 1.125 0 0 1 1.45-.12l.737.527c.35.25.807.272 1.204.107.397-.165.71-.505.78-.929l.15-.894Z" />
              <path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z" />
            </svg>

            </span>
          </a>
        </div>

        <div className="hidden border-b border-dashed lg:block dark:border-neutral-700/70 border-neutral-200"></div>

        <div className="relative pl-3 my-5 overflow-y-scroll">
          <div className="flex flex-col w-full font-medium">

          <div className="block pt-5 pb-[.15rem]">
              <div className="px-4 py-[.65rem]">
                <span className="font-semibold text-[0.95rem] uppercase dark:text-neutral-500/80 text-secondary-dark">Credit</span>
              </div>
            </div>

            <div>
              <span className="select-none flex items-center px-4 py-[.775rem] cursor-pointer my-[.4rem] rounded-[.95rem]">
                <Link to="proposals" className="flex items-center flex-grow text-[1.15rem] dark:text-slate-50 hover:text-slate-300">Proposals</Link>
              </span>
            </div>

            <div>
              <span className="select-none flex items-center px-4 py-[.775rem] cursor-pointer my-[.4rem] rounded-[.95rem]">
                <Link to="credit-cards" className="flex items-center flex-grow text-[1.15rem] dark:text-slate-50 hover:text-slate-300">Credit cards</Link>
              </span>
            </div>
          </div>
        </div>
      </aside>
      <div className="lg:col-span-3">
        <div className="flex shrink-0 px-8 items-center justify-between h-[96px]">
          
        </div>
          <div className='p-5'>
            <Outlet/>
          </div>
      </div>
    </div>
  );
}

export default DashboardPage;
