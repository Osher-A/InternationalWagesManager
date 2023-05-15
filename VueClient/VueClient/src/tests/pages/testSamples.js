import { flushPromises, mount } from '@vue/test-utils'
import {test, expect, vi,} from 'vitest'
import EmployeesIndex from '../../../pages/employees/EmployeesIndex.vue'
import {ref, defineComponent } from 'vue';
import { waitFor, render, screen} from '@testing-library/vue'
import {BaseSpinner} from '../../../components/BaseSpiner.vue'
import {BaseDialog} from '../../../components/BaseDialog.vue'
import {BaseTable} from '../../../components/BaseTable.vue'
import {BaseDiv} from '../../../components/BaseDiv.vue'
import {BaseHeader} from '../../../components/BaseHeader.vue'
import axios from 'axios';


test('test', async () => {
  const wrapper = mount(EmployeesIndex, {
    setup() {
      const employees = ref([])
      return { employees }
    }
  })
  expect(wrapper.employees).toBeTruthy
})

const mockEmployeesList = [
  {
    id: '1',
    firstName: 'osher',
    lastName: 'moscovitch',
    dob: new Date(2000, 6, 4),
    phone: '02045678',
    email: 'oa@.com'
  },
  {
    id: '2',
    firstName: 'chava',
    lastName: 'moscovitch',
    dob: new Date(2005, 11, 13),
    phone: '14567896',
    email: 'c@.com'
  }
]

const mockFetch = vi.spyOn(axios, 'get').mockResolvedValue(mockEmployeesList)

test('testing axios', async () => {
  const wrapper = mount(EmployeesIndex, {
    setup() {
      const employees = ref(mockEmployeesList)
      return employees
    }
  })
  await flushPromises()
  expect(wrapper.findAll('[data-test="employees"]')).toHaveLength(0)

  expect(axios.get).toHaveBeenCalledTimes(1)

  expect(wrapper.text()).toContain('employee!')
  await flushPromises()
  expect(wrapper.vm.employees).toHaveLength(0)
  mockFetch.mockClear()
})

test('testing with Testing Library', async () => {
  render(EmployeesIndex)

  await waitFor(() => expect(mockFetch).toBeCalledTimes(1))
  expect(screen.findAll('[data-test="employees"]')).toHaveLength(0);
  await flushPromises()
  await waitFor(() => {
    expect(screen.findAllByTestId('employees'))
  })
})

test('one last try', async () => {
  const TestComponent = defineComponent({
    components: { EmployeesIndex, BaseDialog, BaseHeader, BaseTable, BaseDiv, BaseSpinner },
    template: '<EmployeesIndex><Suspense><BaseDiv/></Suspense></EmployeesIndex>'
  })
  const wrapper = mount(TestComponent)
  await flushPromises()
  expect(wrapper.findAll('[data-test="employees"]')).toHaveLength(0)
})