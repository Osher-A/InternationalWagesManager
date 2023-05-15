import { mount } from '@vue/test-utils'
import { describe, test, expect} from 'vitest'
import BaseDialog from '../../components/ui/BaseDialog.vue'

describe('BaseDialog', async () => {
    test('when show false, dialog not rendered', () =>{
      const wrapper = mount(BaseDialog, {
        props: {
            show: false
        }
      })
      const dialog = wrapper.find('data-test="dialog"');
      expect(dialog.exists()).toBeFalsy();
    })

    test('when show true, dialog rendered', async () =>{
        const wrapper = mount(BaseDialog);
        await wrapper.setProps({show: true, title: 'The test title'});

        const dialog = wrapper.find('[data-test="dialog"]');
        expect(dialog.exists()).toBeTruthy();
        expect(dialog.text()).toContain('The test title');
      })
})
