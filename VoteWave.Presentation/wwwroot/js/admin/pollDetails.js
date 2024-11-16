$(document).ready(function () {
    // Initialize DataTable
    $('#optionsTable').DataTable();

    // Function to calculate and update percentages
    function updatePercentages() {
        let totalVotes = 0;

        // Calculate total votes
        $('#optionsTable tbody tr').each(function () {
            const votes = parseInt($(this).find('.vote-count').text());
            totalVotes += votes;
        });

        // Update percentages and progress bars
        $('#optionsTable tbody tr').each(function () {
            const votes = parseInt($(this).find('.vote-count').text());
            const percentage = totalVotes > 0 ? ((votes / totalVotes) * 100).toFixed(2) : 0;

            const progressBar = $(this).find('.percentage-bar');
            progressBar.css('width', `${percentage}%`);
            progressBar.attr('aria-valuenow', percentage);
            progressBar.text(`${percentage}%`);
        });
    }

    // Initial percentage calculation
    updatePercentages();

    // Add Option Button
    $('.btn-add-option').click(function () {
        const newOptionRow = `
            <tr>
                <td><input type="text" class="form-control option-edit" placeholder="Enter option text" /></td>
                <td><span class="vote-count">0</span></td>
                <td>
                    <div class="progress">
                        <div class="progress-bar bg-success percentage-bar" role="progressbar" style="width: 0%;" 
                             aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">0%</div>
                    </div>
                </td>
                <td>
                    <button class="btn btn-success btn-sm btn-save-new">
                        <i class="bi bi-check"></i> Save
                    </button>
                    <button class="btn btn-secondary btn-sm btn-cancel-new">
                        <i class="bi bi-x"></i> Cancel
                    </button>
                </td>
            </tr>`;
        $('#optionsTable tbody').append(newOptionRow);
    });

    // Save New Option
    $(document).on('click', '.btn-save-new', function () {
        const row = $(this).closest('tr');
        const newText = row.find('.option-edit').val();

        $.ajax({
            url: '/Admin/AddOption',
            type: 'POST',
            data: { text: newText },
            success: function (newOptionId) {
                row.find('.option-edit').replaceWith(`<span class="option-text">${newText}</span>`);
                row.find('.btn-save-new, .btn-cancel-new').remove();
                row.append(`
                    <button class="btn btn-warning btn-sm me-2 btn-update" data-id="${newOptionId}">
                        <i class="bi bi-pencil"></i> Update
                    </button>
                    <button class="btn btn-danger btn-sm me-2 btn-remove" data-id="${newOptionId}">
                        <i class="bi bi-trash"></i> Remove
                    </button>
                `);
                updatePercentages(); // Recalculate percentages after addition
            },
            error: function () {
                alert('Failed to add option. Please try again.');
            }
        });
    });

    // Update Option Button
    $(document).on('click', '.btn-update', function () {
        const row = $(this).closest('tr');
        const pollId = $(this).data('pollid'); // Get pollId from btn-update

        // Attach pollId to btn-save and btn-cancel
        row.find('.btn-save, .btn-cancel').data('pollid', pollId);
        row.find('.option-text').addClass('d-none');
        row.find('.option-edit').removeClass('d-none');
        row.find('.btn-update, .btn-remove').addClass('d-none');
        row.find('.btn-save, .btn-cancel').removeClass('d-none');
    });

    // Save Updated Option
    $(document).on('click', '.btn-save', function () {
        const row = $(this).closest('tr');
        const optionId = $(this).data('id');
        const pollId = $(this).data('pollid');
        const updatedText = row.find('.option-edit').val();

        $.ajax({
            url: '/Admin/UpdateOption',
            type: 'POST',
            data: { pollId: pollId, optionId: optionId, newText: updatedText },
            success: function (partialViewHtml) {
                // Replace the current row with the updated partial view
                row.replaceWith(partialViewHtml);
                alert('Option updated successfully!');
            },
            error: function () {
                alert('Failed to update option. Please try again.');
            }
        });
    });


    // Cancel Update
    $(document).on('click', '.btn-cancel', function () {
        const row = $(this).closest('tr');
        row.find('.option-text').removeClass('d-none');
        row.find('.option-edit').addClass('d-none');
        row.find('.btn-save, .btn-cancel').addClass('d-none');
        row.find('.btn-update, .btn-remove').removeClass('d-none');
    });

    // Remove Option Button
    $(document).on('click', '.btn-remove', function () {
        const optionId = $(this).data('id');
        const row = $(this).closest('tr');

        if (confirm('Are you sure you want to remove this option?')) {
            $.ajax({
                url: '/AdminPoll/RemoveOption',
                type: 'POST',
                data: { id: optionId },
                success: function () {
                    row.fadeOut(300, function () {
                        $(this).remove();
                        updatePercentages(); // Recalculate percentages after removal
                    });
                },
                error: function () {
                    alert('Failed to remove option. Please try again.');
                }
            });
        }
    });

    // Cancel Adding New Option
    $(document).on('click', '.btn-cancel-new', function () {
        $(this).closest('tr').remove();
    });
});
